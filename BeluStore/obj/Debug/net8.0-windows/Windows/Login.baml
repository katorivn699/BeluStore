   M S B A M L   `   `   `  ����  	BeluStore    BeluStore.Windows.Login   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   A =Wpf.Ui, Version=3.0.5.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ Login��5	   	   $	��350��6   $	��350��6$   .��5
      �� .+�5   
     Wpf.Ui.Controls.TitleBar     Title$ 	BeluStore��6   �� 5   
   $��Login��6   $��Center=�6!   $��24(�6>     Wpf.Ui.Controls.Card  5   
   $��8q�6   .��5        Wpf.Ui.Controls.TextBox     PlaceholderText$ Username��6X      PlaceholderEnabled$
 True��6s   ��6   �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�6s   5        5   
   $��8q�6   .��5      !  Wpf.Ui.Controls.PasswordBox  -   5          Name$ PasswordBox��5      $ Password��5      $
 True��5         Password 5      �� *
Password+$ PropertyChanged=� �� Mode$ TwoWay=�5      5      �� 5   
   $��Login��5      $��Center=�5      $	��100��5      ��5      �� *LoginCommand+��5      �� 	 �� ElementName$	 PasswordBox��
 �� Path$
 Password��� 5   
   -   5        $*��$Don't have an account? Register here��5      $��Center=�5      5      �� 5   
   -   5        $��ErrorMessage��5      
���  ��$��12(�69   $��Center=�6G   $��5q�6d   5      5       