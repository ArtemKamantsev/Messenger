   M S B A M L   `   `   `  ����D  @Messenger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:Messenger	Messenger  5   	       Messenger.ChatWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:Messenger     5   	   -   5          � Title$  
ChatWindow��5   	   $	��450��5	   	   $	��800��6    � WindowStartupLocation$ CenterScreen=�5   	      CommonWindow#��[  5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      6   �� 5      $
��auto �6   .+�5   
   �� -   5        $��tbFriendName��5      $��Chat with: userName��6(   $��10 0 0 0q�6C   
�������$��Center=�6h   $��25(�6�   �� 5   
   -   5        $��tbClose��5        CloseBtn#��[ 5      5      �� 5   
   -   5         ��Name$ messageList��5      $��1��5      $��2��6   $��Stretch=�63   ��5      Z� ��5      Z� ��5      �� 5      .R�5      � $��0 2 0 2q�6   .+�5      �� ���	#FF0088CC �� CornerRadius$ 5��68   ��6J   �� *	Alignment+._�5       �� $��5 2 5 2q�6A   $��15(�6h   $
��Bold"�6v   ��6(   �� *	Content+��6R   Z� 6v   5!      5"      5#      ��5%      �� 5&       �� 
TargetType" ��6   .�5'      ��  �� Property � �� �� Value
 ���� 63   �� 5(       � �� 5)      �� 5*       �� 
TargetType" ��6.   .R�5+   "   �� 5,      5.      5/      51      �� 52   
   $��2��6   $ 10��6   ���	#FFCCCCCC$��5 0 5 5q�6G   ._�53      � 	 � ColumnDefinitions	 54      �� 55      �� 56      $
��auto �6'   .+�58      �� -   5        $��1��58      $��30��6(   $��/images/send.png��64   $��0 0 5 0q�6N   $��Right=�6_   6|   �� 59      -   5        $��	tbMessage��59   &   $��2q�6   $��2��67   $��0q�6D   $��25(�6X   ���	#FFCCCCCC$��10 0 5 0q�6}   5:      5;      5<   	   5=      