   M S B A M L   `   `   `  ����N  JPB_Semester_Project, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:PB_Semester_ProjectPB_Semester_Project  5   	       PB_Semester_Project.Bases   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:PB_Semester_Project     5   	   -   5          � Title$  Bases��5   	   $	��600��6   $
��1220��6$    � WindowStartupLocation$ CenterScreen=�6J   .��5	      � .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ baseProductsGrid��5
      $
��Left=�6-   $	��492��6H   $	��Top=�6U   $	��526��6m   $��
294,62,0,0q�6�     5   
   -   5        $ 	basesGrid��5      $
��Left=�6&   $	��440��6A   $��10,0,0,0q�6N   $��Center=�6`   $	��279��6�   �� 5   
   $��Товари бази��6   $��Center=�6&   $��52��6C   $��0,10,0,0q�6O   $	��Top=�6a   $	��612��6y   $��24(�6�   $��Center=�6�   $��Center=�6�   �� 5   
   $%��Продовольчі бази��6   $
��Left=�6+   $��47��6F   $��	10,10,0,0q�6R   $	��Top=�6e   $	��279��6}   $��24(�6�   $��Center=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ 
chooseBase��5      $��Обрати��6%   $
��Left=�66   $��47��6Q   $��
10,507,0,0q�6]   $	��Top=�6q   $	��279��6�   6�   �� 5   
   $!��Замовити товар��6   $
��Left=�6)   $��36��6D   $��888,111,0,0q�6P   $	��Top=�6e   $	��272��6}   $��20(�6�   $��Center=�6�   $��Center=�6�   �� 5   
   $%��Виберіть магазин��6   $
��Left=�6+   $��36��6F   $��884,156,0,0q�6R   $	��Top=�6g   $	��135��6   $��14(�6�   $��Center=�6�   $��Center=�6�   �� 5   
   -   5         ��Name$ storesCombo��5      $
��Left=�6(   $��35��6C   $��884,197,0,0q�6O   $	��Top=�6d   $	��135��6|   6�   �� 5   
   $)��#Виберіть кількість��6   $
��Left=�6-   $��29��6H   $��888,259,0,0q�6T   $	��Top=�6i   $	��116��6�   �� 5   
   -   5         ��Name$ amountOfProduct��5      $
��Left=�6+   $��36��6F   $��888,290,0,0q�6R    �� TextWrapping$
 Wrap=�6g   $	��Top=�6{   $	��116��6�   $��18(�6�   $��Center=�6�   $
��Left=�6�   �� 5   
   $"��Ціна за одиницю��6   $
��Left=�6*   $��36��6E   $��1049,252,0,0q�6Q   $	��Top=�6g   $	��127��6   $��14(�6�   $��Center=�6�   $��Center=�6�   �� 5   
   -   5        $ pricePerUnit��5      $
��Left=�6(   $��36��6C   $��1049,290,0,0q�6O   $
 Wrap=�6e   $	��Top=�6y   $	��127��6�   $��18(�6�   $��Center=�6�   $
��Left=�6�   �� 5   
   -   5        $ 	makeOrder��5      $��Замовити��6$   $
��Left=�67   $��42��6R   $��844,512,0,0q�6^   $	��Top=�6s   $	��364��6�   6�   �� 5   
   $#��Виберіть відділ��6   $
��Left=�6*   $��36��6E   $��1041,156,0,0q�6Q   $	��Top=�6g   $	��135��6   $��14(�6�   $��Center=�6�   $��Center=�6�   �� 5   
   -	   5        $ storeDepartmentCombo��5      $
��Left=�61   $��35��6L   $��1041,197,0,0q�6X   $	��Top=�6n   $	��135��6�   5      5      