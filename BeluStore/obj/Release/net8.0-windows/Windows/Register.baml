   M S B A M L   `   `   `  ����   	BeluStore     BeluStore.Windows.Register   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     A =Wpf.Ui, Version=3.0.5.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml    � 
ResizeMode$  NoResize=� � Title$ Register��$	��690��$	��350��.���� .+�  Wpf.Ui.Controls.TitleBar     Title$ Register���� $��Create Account��$��Center=�$��24(�  Wpf.Ui.Controls.Card  $��8q�.��  Wpf.Ui.Controls.TextBox     PlaceholderText$ Username��   PlaceholderEnabled$
 True������ *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�  $��8q�.��  $ Email��$
 True������ *Email+$ PropertyChanged=�  $��8q�.��!  Wpf.Ui.Controls.PasswordBox  -     Name$ PasswordBox��$ Password��$
 True��   Password �� *
Password+$ PropertyChanged=� �� Mode$ TwoWay=�  $��8q�.��  $ 	Full Name��$
 True������ *
FullName+$ PropertyChanged=�  $��8q�.��  $ Phone Number��$
 True������ *PhoneNumber+$ PropertyChanged=�  $��8q�.��  $ Address��$
 True������ *	Address+$ PropertyChanged=��� $��Register��$��Center=�$	��100������ *RegisterCommand+���� 	 �� ElementName$	 PasswordBox��
 �� Path$
 Password��� -   $��ErrorMessage��
���  ��$��12(�$��Center=�$��5q�