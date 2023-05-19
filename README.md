# Winform.AutoSizeHelper

## Introduction
A Control Layout Adaptive Resolution Assistant Class for Winform.  

## Download
- GitHub: https://github.com/hlz2516/Winform.AutoSizeHelper 
- Nuget:  https://www.nuget.org/packages/Winform.AutoSizeHelper 

## Function
- When the size of container controls changes, the controls inside the container are arranged adaptively according to their original layout
- When dynamically adding a new control within the container, the size and position can be adjusted to fit the layout by calling methods
- If there are nested layouts within the layout, the nested layouts will also be adaptive

## How To Use

### Basic Use
1. Design your Form in Form Designer,for example:  
![step1](./pictures/step1.png)

2. Create  a AutoSizeHelper and set the container in Form1's constructor
```
AutoSizeHelper.AutoSizeHelper helper;
public Form1()
{
    InitializeComponent();
    helper = new AutoSizeHelper.AutoSizeHelper();
    helper.SetContainer(this);
}
```

3. Run your application,and maximize the application window,then you can see:
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
    //calcul button6's font rate and apply this rate to newBtn's font
    float fontRate = button6.Font.Size / this.Height;
    float newBtnSize = (float)Math.Round(this.Height * fontRate);
    newBtn.Font = new Font(button6.Font.FontFamily, newBtnSize);
    newBtn.UseVisualStyleBackColor = true;
    this.Controls.Add(newBtn);
    helper.AddNewControl(newBtn);
    helper.UpdateControls();
}
```

2. Run the application,click button6,you can see button7 showed between button2 and button3.
then you can maximize or minimize the application window,the button7 always adapt to the current layout.