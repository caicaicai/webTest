   M S B A M L   `   `   `  ����   webTest    webTest.View.OptionWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  OptionWindow��$	��700��$	��800�� � WindowStartupLocation$ CenterOwner=�.��� .+��� .@��� $��General��.���  � RowDefinitions �� $��1* ��� $��2* �.+��� $��Simulate��$��0��$��
10,10,10,5q�.���  �� $��3* ��� $��2* �.+�� $��0��.+��� $
��Left=�$	��top=�$��0,10,0,0q�.���� $��Cookie�� d� 	IsChecked �� *option.UseCookie+�� $��120,10,10,10q� �� TextWrapping$
 Wrap=����� *option.Cookie+� $��1��.+��� $
��Left=�$	��Top=�$��0,10,0,0q�.���� $��	UserAgent�� �� *option.UseUserAgent+�� $��120,14,10,6q�$
 Wrap=����� *option.UserAgent+��  �� SelectedValuePath$ Value��$
��Left=�$��5,34,0,0q�$	��Top=�$	��100������ *option.UserAgentTemplate+ �� SelectedValue ��  �� Path$ SelectedUserAgent� �� Mode$ TwoWay=����� .R��� ���� �� $!��Special Configuration Items��$��1��$��
10,10,10,5q�.���  �� $��40 ��� $��40 ��� $��40 �.+�� $��0��.+��� $
��Left=�$	��top=�$��0,5,0,5q�.��UseUriRewrite�� $��
120,5,10,5q�$
 Wrap=�$��Center=����� *option.UriRewrite+� $��1��.+��� $
��Left=�$	��top=�$��0,5,0,5q�.��Host�� $��
120,5,10,5q�$
 Wrap=�$��Center=����� *option.Host+� $��2��.+��� $
��Left=�$	��top=�$��0,5,0,5q�.��	TimeOut�� $��120,5,555,5q�$
 Wrap=�$��Center=����� *option.Timeout+�� -   	 ��Name$	 label��$$��ms(0 为不设置超时时间)��$
��Left=�$��	200,5,0,0q�$	��Top=�$	��143��$��31���� $��Proxy��.��� .+��� $��Proxy��$��2��$��
10,5,10,10q�.��� 
 � ColumnDefinitions
 �� $	��200 ��� $��* �.+�� $��0��$��0,0,10,0q� �� $��30 ��� $��* ��� $��30 ��� $��30 �.+��� $��0��$��Bottom=�.��Proxy Server List�� $��1������ *option.Proxys+ �� SelectedIndex �� *option.SelectedProxyIndex+���� .R��� ���� *Server+� $��2��
 �� $��1* ��� $��1* ��� $��1* �.+��� $��0��$	��Add��$��0,3,3,3q����� *
AddProxy+�� $��1��$��Remove��$��0,3,0,3q����� *RemoveProxy+�� $��2��$��Check��$��3,3,0,3q����� *
CheckProxy+� $��3��
 �� $��1* ��� $��1* ��� $��1* �.+��� $��0��$��Import��$��0,3,3,3q����� *ImportProxys+�� $��1��$��Export��$��0,3,0,3q����� *ExportProxys+�� $��2��$��Clear��$��3,3,0,3q����� *ClearProxys+�� $��1��
�������$��1,0,0,0q�._�� $��10,0,0,0q� �� $��30 ��� $��* ��� $��30 ��� $��30 �.+�� $��0��.+�� 
 �� $	��140 ��� $��* �.+��� 
�������$��1,1,0,1q�._��� $��0��$��Bottom=�$��Right=�$��0,0,15,5q�.��Target Server� $��1��$��Stretch=�.+��� $��Center=����� *ProxySpider.TargetServer+� $��1��.+��� $��10q����� *ProxySpider.SpiderLog+�� $��2��
�������$��1,0,1,0q�._�� 
 �� $��* ��� $	��100 ��� $	��100 ��� $	��100 ��� $	��100 �.+��� $��1��$��Right=�$��Center=�.��Depth�� -   $��2��$
��left=�$��Center=����� *ProxySpider.Depth+�� $��Add To List��$��3������ *PushIpToOption+�� $��Crawling��$��4������ *	RunSpider+�� $��3��
�������$��1,1,1,1q�._�� 
 �� $��* ��� $	��130 ��� $	��130 �.+��� $��1��$��5q� �� *option.UseProxy+.��Enable Proxy�� $��2��$��5q� �� *option.AutoNextProxy+.��Auto Next Proxy�� $��Others��