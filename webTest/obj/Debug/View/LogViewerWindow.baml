   M S B A M L   `   `   `  ����B  >webTest, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:webTest.ViewwebTest.View  5   	   "    webTest.View.LogViewerWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:webTest.View     5   	     � Title$  LogViewerWindow��5   	   $	��400��6!   $	��600��6.   .��5	      �  � RowDefinitions 5
   
   �� 5      $��25 �6   �� 5      $��* �6   .+�5   
   � $��0��6   .+�5      �� -   5         ��Name$ button��5      $��Clear��6?   $
��Left=�6O   $��2q�6j   $	��Top=�6u   $��75��6�   ��6%   �� *Clear+6�   5      � 5   
   $��1��6   .+�5      �� -   5        $��MyScrollViewer��5      .��5      �� -   5        
���   ����	#FFC2F70E��.  � ForceCursor .  �� 
AcceptsTab .  �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�5   �    �� 
IsReadOnly .  �� TextWrapping$ WrapWithOverflow=�6  ��6   �� *logger.LogContent+6  5      5      5      5      