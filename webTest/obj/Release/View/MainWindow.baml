   M S B A M L   `   `   `  ����   webTest    webTest.View.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     X TXceed.Wpf.Toolkit, Version=2.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47toolkit)http://schemas.xceed.com/wpf/xaml/toolkit  c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  :d2http://schemas.microsoft.com/expression/blend/2008  Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  -     � Title$  webTest��$	��650��$	��800��$
��Auto=�$	��650��$	��650�� � 	Resources    MyButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property	 P� Value��%  &	        �� " ��.���  �@�OverridesDefaultStyle . ��  �@�Cursor$
 Hand����  � �� �� " ��.R��� $ border��$��0q�
���   �#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver	 . . ���  �@J�Opacity$	 0.8Q�
 � DataContext
 ��  �� Path$
 Main�  Locator �� Source# [ .���  � RowDefinitions �� $��22 ��� $��4* ��� $��6 ��� $��3* �.+�� $��0��.+�o�  o� 
IsMainMenu . ���	#FFA4A2A2$��0,0,0,1q�.@�m� $��_File��.@�m� $
��_New�� m� Command �� *New+m� $��_Open�� �� *Open+m� $��_Save�� �� *Save+m� $��Save As ...�� �� *SaveAs+m� $��Option�� �� *
OpenOption+m� $��
_LogViewer�� �� *	LogViewer+m� $��_About�� �� *About+� $��1��.+��� $��Control��.��� $��	10,10,0,0q� � ColumnDefinitions �� $	��200 ��� $��20 ��� $	��70* �.+�� $��0��.+��� $��0,0,0,28q�$��Stretch=����� *TabItemsGroup+ �� SelectedIndex �� *SelectedGroupIndex+���� .R�� .+��� $��Center=����� *Name+ J� InputBindings d�  d� MouseAction$ LeftDoubleClicke� �� Command �� *!DataContext.TabItemsGroupRename+ �� RelativeSource �� *FindAncestor+ �� AncestorType" � �� CommandParameter �� *!(ItemsControl.AlternationIndex)+ ��  �� Mode$ TemplatedParent=� � ContextMenu �� .@�m� $
��test��m� $
��test���� ���� *Name+y��� *	IsEditing+.�� �� 
 �� Key$ Return�� �� *DataContext.SaveGroupName+ �� *FindAncestor+" ��� $ Enter�� �� *DataContext.SaveGroupName+ �� *FindAncestor+" �.��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ 	LostFocus��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *DataContext.SaveGroupName+ �� *FindAncestor+" �.���  �� $��1* ��� $��1* �.+��� $	��New��$��0��$��23��$��Bottom=�$��0,0,10,0q����� *
AddGroup+�� $��Delete��$��1��$��23��$��Bottom=�$��0,0,10,0q����� *DeleteGroup+�� $��1��$��Stretch=�$��Center=�$��2��$��0q�$��0q�� $��2��
 �� *" CurrentTabItemsGroup.CurrentItem+.+�� $��0,0,10,0q� �� $��30 ��� $��30 ��� $��30 ��� $��30 ��� $��8 ��� $��* ��� $��30 �.+�� $��0q�$��0��.+��� $
��Left=�$��Right=�$	��Top=�$	��120��$��25������ *MainLab.TITLE+�� $��25��$	��Top=�$��Center=�$��	125,0,0,0q����� *Title+� $��0,0,0,5q�$��1��.+��� $
��Left=�$��Right=�$	��Top=�$	��120��$��25������ *MainLab.URL+�� -   $��25��$	��Top=�$��Center=�$��	125,0,0,0q����� *
RequestUrl+� $��0,0,0,5q�$��2��.+��� $
��Left=�$��Right=�$	��Top=�$	��120��$��25������ *MainLab.QUERYSTR+�� $��25��$	��Top=�$��Center=�$��	125,0,0,0q����� *
QueryStr+� $��0,0,0,0q�$��3��.+��� $
��Left=�$��Right=�$	��Top=�$	��120��$��25������ *MainLab.POSTDATA+�� $	��Top=�$��	125,0,0,0q� �� AcceptsReturn .   �� 
AcceptsTab  . ���� *
PostData+�� ! �� ResizeDirection$
! Rows=�$��4��$
��Auto��$��2��$��Stretch=�$��Stretch=�$��0q�� $��0,0,0,0q�$��5��.+��� $
��Left=�$��Right=�$	��Top=�$	��120��$��25������ *MainLab.NOTE+#  Xceed.Wpf.Toolkit.RichTextBox  -   "  Name$" _richTextBox��$
��Auto=�$��	125,0,0,0q�#   Text# �� *Note+$   TextFormatter$ $  Xceed.Wpf.Toolkit.RtfFormatter  � $��0,5,0,0q�$��6�� �� $��* ��� $��80 ��� $	��100 ��� $	��100 ��� $��80 ��� $	��100 �.+�� $��2��.+��  �� $��1* ��� $��1* �.+��� $��0��$��thread:���� $��1��$�� ��� $��3��.+��  �� $��1* ��� $��1* �.+��� $��0��$��times:���� $��1��$��Center=����� *Times+� $��4��$��0,0,5,0q�.+��� $��1��$��Center=�$��Center=�$��0q�$��25������ *MethodListData+% �� SelectedItem% �� *	ReqMethod+���� .R��� ���� � $��5��.+��� -   & ��Name$	& btn��$��25��$��Bottom=�$��0,0,0,0q����� *RequestButton+���� *DataContext.ShowPopUp+ �� *FindAncestor+" ��� $
! Rows=�$��2��$
��Auto��$��2��$��Stretch=�$��Stretch=�$��0q�� $��3��.+��� $��Result��$��0q�.���  �� $��30 ��� $��* �.+��� $��	10,0,10,0q����	#FFB9B9B9$��1q�._�� $��0�� �� $��70 ��� $��70 ��� $��70 ��� $��* ��� $	��100 �.+�� $��0��.+��� $�� ��$
��JSON��$��0,0,0,0q����	#FFC3C0C0$��0,0,1,0q�#��[  ���� *SpecialView+���� ' �� ImageSource$' /resource/json1.png��� $��1��.+��� $�� ��$	��XML��$��0,0,0,0q����	#FFC3C0C0$��0,0,1,0q�#��[  ���� *SpecialView+���� $' /resource/xml1.png��� $��2��.+��� $�� ��$
��HTML��$��0,0,0,0q����	#FFC3C0C0$��0,0,1,0q�#��[  ���� *SpecialView+���� $' /resource/html.png��� $��3��.+��� $��UriRewrite :��$	��100��$
��Left=��� $��	100,0,0,0q�$
��Left=����� *option.UriRewrite+� $��4��� $��1��.+��� ( �� TabStripPlacement$( Bottom=�$��
10,5,10,10q�$��Disabled=����� *CurrentTabItemsGroup.TabItems+ �� *'%CurrentTabItemsGroup.SelectedTabIndex+���� .R��� ���� *Title+ d� $ 
RightClicke� �� *DataContext.DeleteTab+ �� *FindAncestor+" �) �� ContentTemplate) �� .R��� $��0q�"* �� VerticalScrollBarVisibility$
* Auto=�+ �� TextWrapping$
+ Wrap=� . ���� *ResponseContent+