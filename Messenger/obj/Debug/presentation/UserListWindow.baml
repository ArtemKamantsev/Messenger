   M S B A M L   `   `   `  ����D  @Messenger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Messenger	Messenger  5   	       Messenger.UserListWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Messenger     5   	   -   5          � Title$  Users online��5   	   $	��450��5	   	   $	��800��6    � WindowStartupLocation$ CenterScreen=�5   	      CommonWindow#��[  5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      .+�5   
   �� $��Users online:��6   $��10 0 0 0q�6)   
�������$��Center=�6N   $��25(�6i   �� 5   
   -   5        $��tbClose��5        CloseBtn#��[ 5      5      �� 5   
   -   5         ��Name$ userList��5      $��Stretch=�5      $��1��5      ��5      Z� ��5      Z� ��5      �� 5      .R�5      � ���	#88FFFFFF � ColumnDefinitions 5      �� 5       �� 5!      $
��auto �6/   .+�5#      �� $��30(�6;   $
��Bold"�6I   $��5 0 0 0q�6[   ��6$   �� *Name+6[   �� 5$      $��0 0 3 0q�6A   $��1��6R   $��20(�6b   $
��Bold"�6p   
����ح���6$   �� *UnreadCount+��5%      �� 5&   "    �� 
TargetType" ��6(    �� Triggers 5'   &   �� 5(   *    �� Binding 66   �� *UnreadCount+ �� Value 06V   .a�5)   .   �� 	 �� Property	 � y�
 �� Value$
 Hidden=�6K   5*   +   5,   #   5.      5/      50      52      �� 53   
   -   5         ��Name$ placeholder��53      $��1��54      $��There is no users��55      $��Center=�56      $��Center=�57      $��40(�58      $y�Hidden=�59      5:      5;      