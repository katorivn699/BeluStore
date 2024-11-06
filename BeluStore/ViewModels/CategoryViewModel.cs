using BeluStore.Models;
using BeluStore.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BeluStore.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        private Category selectedCategory;
        public Category SelectedCategory
        {
            get { return selectedCategory; }
            set
            {   
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    EditedCategory = selectedCategory != null ? CloneCategory(selectedCategory) : null;
                    OnPropertyChanged();
                }
            }
        }

        private Category editedCategory;
        public Category EditedCategory  
        {
            get { return editedCategory; }
            set
            {
                if (editedCategory != value)
                {
                    editedCategory = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _searchKeyword;
        public string _SearchKeyword
        {
            get { return _searchKeyword; }
            set
            {
                if (_searchKeyword != value)
                {
                    _searchKeyword = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Category> categories { get; set; }

        public ICommand AddCategoryCommand { get; set; }
        public ICommand DeleteCategoryCommand { get; set; }
        public ICommand UpdateCategoryCommand { get; set; }
        public ICommand ClearCategoryCommand { get; set; }
        public ICommand SearchCategoryCommand { get; set; }

        public CategoryViewModel()
        {
            LoadCategories();
            SelectedCategory = new Category();
            EditedCategory = new Category();

            SearchCategoryCommand = new RelayCommand(searchCategory);
            AddCategoryCommand = new RelayCommand(AddCategory);
            DeleteCategoryCommand = new RelayCommand(DeleteCategory);
            UpdateCategoryCommand = new RelayCommand(UpdateCategory);
            ClearCategoryCommand = new RelayCommand(ClearCategory);
        }

        private void LoadCategories()
        {
            using (var context = new BeluStoreContext())
            {
                var CategoryList = context.Categories.ToList();
                categories = new ObservableCollection<Category>(CategoryList);
            }
        }

        public void AddCategory(object param)
        {
            bool check = ValidateInputs();
            if (check)
            {
                using (var context = new BeluStoreContext())
                {
                    EditedCategory.CategoryId = 0;
                    context.Categories.Add(EditedCategory);
                    context.SaveChanges();

                    categories.Add(EditedCategory);
                }

                ClearCategory(null);
            }
        }

        public void UpdateCategory(object param)
        {
            if (SelectedCategory != null && EditedCategory != null)
            {
                using (var context = new BeluStoreContext())
                {
                    var categoryToUpdate = context.Categories.Find(SelectedCategory.CategoryId);
                    if (categoryToUpdate != null)
                    {
                        categoryToUpdate.CategoryId = EditedCategory.CategoryId;
                        categoryToUpdate.CategoryName = EditedCategory.CategoryName;
                        categoryToUpdate.Description = EditedCategory.Description;

                        context.SaveChanges();

                        var index = categories.IndexOf(SelectedCategory);
                        categories[index] = categoryToUpdate;
                    }
                }
            }
        }

        public void DeleteCategory(object param)
        {
                if (SelectedCategory != null)
                {
                    using (var context = new BeluStoreContext())
                    {
                        var categoryToDelete = context.Categories.Find(SelectedCategory.CategoryId);
                        if (categoryToDelete != null)
                        {
                            context.Categories.Remove(categoryToDelete);
                            context.SaveChanges();

                            categories.Remove(SelectedCategory);
                            ClearCategory(null);
                        }
                    }
                }                 
        }

        public void ClearCategory(object param)
        {
            SelectedCategory = null;
            EditedCategory = new Category();
            OnPropertyChanged(nameof(SelectedCategory));
            OnPropertyChanged(nameof(EditedCategory));
        }

        private Category CloneCategory(Category category)
        {
            return new Category
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                Description = category.Description,
            };
        }
        private void searchCategory(object param)
        {
            using (var context = new BeluStoreContext())
            {
                var filteredCategories = context.Categories
                    .Where(s => s.CategoryName.Contains(_SearchKeyword) || s.Description.Contains(_SearchKeyword))
                    .ToList();
                categories = new ObservableCollection<Category>(filteredCategories);
                OnPropertyChanged(nameof(categories));
            }
        }
        private bool ValidateInputs()
        {
            var errorMessages = new List<string>();

            // Validate CategoryName
            if (string.IsNullOrWhiteSpace(EditedCategory.CategoryName))
            {
                errorMessages.Add("Category Name cannot be empty.");
            }

            // Validate Description
            if (string.IsNullOrWhiteSpace(EditedCategory.Description))
            {
                errorMessages.Add("Description cannot be empty.");
            }

            // Show error messages if any
            if (errorMessages.Any())
            {
                MessageBox.Show(string.Join(Environment.NewLine, errorMessages), "Validation Errors", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // All validations passed
            return true;
        }

    }
}
