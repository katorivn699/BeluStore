   M S B A M L   `   `   `  ����D  @BeluStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:BeluStore.ViewsBeluStore.Views  5      ;"clr-namespace:BeluStore.ViewModelsBeluStore.ViewModels  5      "    BeluStore.Views.CategoryPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:BeluStore.Views     5      1vm"clr-namespace:BeluStore.ViewModels     5        N� Title$  CategoryPage��5
       � DataContext 5      ,   &BeluStore.ViewModels.CategoryViewModel  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $��4* �6   �� 5      $��6* �6    � ColumnDefinitions 5   
   �� 5      $��4* �6   �� 5      $��6* �6   .+�5   
   &   System.Windows.Controls.DataGrid  $��0��6   $��0��6    $��2��60      AutoGenerateColumns$ False��5         CanUserAddRows$ False��5          
IsReadOnly$
 True��5!      ��5      �� *
categories+ �� SelectedItem 5      �� *SelectedCategory+   Columns 5"      0  *System.Windows.Controls.DataGridTextColumn  5#      ,  &System.Windows.Controls.DataGridColumn	   Header$	 
CategoryId��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 1* 6X   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *
CategoryId+6X     5$      $	 CategoryName��6%   $
 1* 6\    6;   �� *CategoryName+6\     5%      $	 Description��6%   $
 4* 6Z    6:   �� *Description+6Z   5'      � 5*   
   $��1��6   $��1��6    5+      �� 5,      $��* �6    �� 5-      $��* �6    �� 5.      $��* �6    �� 5/      $��* �6    �� 50      $��* �6    �� 51      $��* �6     53      �� 54      $��* �6#   �� 55      $��* �6#   �� 56      $��* �6#   .+�59      �� $��0��6   $��0��6%   $��
CategoryId��65   $��Center=�6G   $��Center=�6d   
�������6   �� 5:      $��0��6   $��1��6%   $��CategoryName��65   $��Center=�6I   $��Center=�6f   
�������6�   �� 5;      $��0��6   $��2��6%   $��Description��65   $��Center=�6H   $��Center=�6e   
�������6�   �� 5=      $��1��6   $��0��6#    �� 
IsReadOnly$
 True��6k   ��63   �� *EditedCategory.CategoryId+ �� Mode$ OneWay=�6k   �� 5>      $��1��6   $��1��6#   ��63   �� *EditedCategory.CategoryName+$ TwoWay=��� 5?      $��1��6   $��2��6#   ��63   �� *EditedCategory.Description+$ TwoWay=��� 5@      $��3��6   $��0��6%   $��Right=�65   $��Center=�6Q   
�������$��0,0,0,0q�6   	Search:6�   �� 5A      $��3��6   $��1��6#   $	��200��63   $��40��6?   ��5B      �� *_SearchKeyword+ �� UpdateSourceTrigger$ PropertyChanged=�5C      � 5F   
   $��2��6   $��0��6!   $��Center=�61   $��Center=�6N   .+�5G      �� -   5         ��Name$ 	AddButton��5G      $	��Add��6(   $��50��66   
�������$
��Bold"�6U   
������ $	��100��6�   $��10q�6�   
���2�2���6�   �� *AddCategoryCommand+�� 5H      -   5        $ UpdateButton��5H      $��Update��6+   $��50��6<   
�������$
��Bold"�6[   
������ $	��100��6�   $��10q�6�   ���#005B96��6�   �� *UpdateCategoryCommand+�� 5I      -   5        $ DeleteButton��5I      $��Delete��6+   $��50��6<   
�������$
��Bold"�6\   
������ $	��100��6�   $��10q�6�   ���#FF4C4C��6�   �� *DeleteCategoryCommand+��6�   �� *SelectedCategory+�� 5J      -   5        $ Clear��5J      $��Clear��6$   $��50��64   $	��100��6@   
�������$
��Bold"�6_   
������ $��10q�6�   
���������6�   �� *ClearCategoryCommand+�� 5K      -   5        $ Search��5K      $��Search��6%   $��50��66   $	��100��6B   
�������$
��Bold"�6a   
������ $��10q�6�   
���������6�   �� *SearchCategoryCommand+5L      5M      5O      