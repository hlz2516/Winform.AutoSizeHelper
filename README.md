# Winform.AutoSizeHelper

## Introduction
A Control Layout Adaptive Resolution Assistant Class for Winform.  

## Download
- GitHub: https://github.com/hlz2516/Winform.AutoSizeHelper 
- Nuget:  https://www.nuget.org/packages/Winform.AutoSizeHelper 

## Function
- When the container control changes its size, the controls inside the container are arranged adaptively according to their original layout
- When dynamically adding a new control within the container, the size,position and font can be adjusted to fit the layout by calling methods
- If there are nested layouts within the layout, the nested layouts will also be adaptive
- support autosize the listview(only column width)

## How To Use

### Basic Use
1. Design your Form in Form Designer,for example:  
![step1](./pictures/step1.png)

2. Disable the AutoScaleMode property in Form manually.
 Find Form1.Designer.cs and open it, press Ctrl + F to search string:"AutoScaleMode",then you can see this line:
 ```
 this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
 ```
 Modify it to System.Windows.Forms.AutoScaleMode.None.

3. Create  a AutoSizeHelper and set the container in Form1's constructor
```
using AutoSizeTools;
namespace XXX{
  public partial class Form1 : Form{
    AutoSizeHelper helper;
    public Form1()
    {
        InitializeComponent();
        helper = new AutoSizeHelper(this);
    }
  }
}
```

4. Run your application,and maximize the application window,then you can see:
![step3](./pictures/step3.png)

### Dynamically Adding New Controls
if we want to dynamically Add a new button 
which between button2 and button3 by clicking button6,
we can achieve this in the following way:  
1. double click button6 in form deigner,in the following methed,we write code like this:
```
private void button6_Click(object sender, EventArgs e)
{
    Button newBtn = new Button();
    newBtn.Name = "button7";
    newBtn.Location = new Point(568, 12);
    newBtn.Size = new System.Drawing.Size(75, 23);
    newBtn.Text = "button7";
    //apply button6's font to newBtn font
    newBtn.Font = new Font(button6.Font.FontFamily, button6.Font.Size);
    newBtn.UseVisualStyleBackColor = true;
    this.Controls.Add(newBtn);
    helper.AddNewControl(newBtn);
    helper.UpdateControls();
}
```

2. Run the application,click button6,you can see button7 showed between button2 and button3.
then you can maximize or minimize the application window,the button7 always adapt to the current layout.

### Use AutoSizeHelperEx(recommend)
There are some differences in using AutoSizeHelper and AutoSizeHelperEx.
When using AutoSizeHelperEx, it should be noted that you need to call the UpdateControls method in the sizechanged event handler of the form.Such like this(this example is from demo--adjust_font):
```
private void FontForm_SizeChanged(object sender, EventArgs e)
{
    Size screenSize = Screen.PrimaryScreen.Bounds.Size;
    if (screenSize == new Size(1920,1080)) 
    {
        helper.FontAdjustRate = 1.0f;
    }
    else if (screenSize == new Size(1280, 960))  
    {
        helper.FontAdjustRate = 0.8f; 
    }
    helper.UpdateControls(); // call this method to refresh UI
}
```
The other uses of AutoSizeHelperEx are consistent with AutoSizeHelper.

For more examples, please see github repository.

## Please analyze your UI situation before using it!

After publishing this class library for a period of time, I found that some developers have abused AutoSizeHelper/AutoSizeHelperEx, which has led to some unexpected errors and feedback to me. 
Therefore, I have added this section specifically. I hope that developers can analyze their interface situation before deciding which classes should use adaptive assistance and which classes should not, 
Don't think it's okay if I apply adaptive assistance to every form/control class, things are not as simple as you think!  
Where do you need to use the principles of AutoSizeHelper/AutoSizeHelperEx:  
**After which form/control changes its own size, its internal direct sub-controls (I mean don't care about the sub-controls of sub-controls) remain in its original relative position (intuitive performance is that each direct sub-control The location property remains unchanged), 
these form/control classes need to apply the AutoSizeHelper(Ex), in this case you need to use this class, and call the SetContainer method to set these forms/controls as containers**

## Tips

- If you still can't scale according to the tutorial, please check whether each form/control that applies AutoSizeHelper/AutoSizeHelperEx has set AutoScaleMode to none
Specifically,I added the AutoSizeHelperEx class in version 1.0.5, which allows you to adjust the font scaling ratio on different resolution screens
- The new AutoSizeHelper/AutoSizeHelperEx operation is best followed by the InitializeComponent method. If you want to create AutoSizeHelper/AutoSizeHelperEx after adding some controls, this class library is not guaranteed to work normally
- For more informations please see README.zh-CN.md