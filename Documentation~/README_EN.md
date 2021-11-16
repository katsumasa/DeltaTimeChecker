# DeltaTimeChecker

Program that could visually check the value of Time.deltaTime in graphs.

If you want to set the frame rate with Application.targetFrameRate without synchronizing with the VSYNC count, set targetFrameRate to either -1, a value equal to the screen refresh rate, or a refresh rate divided by an integer. 

When the refresh rate of the device is 90 [Hz] and Application.targetFrameRate = 60, the value of Time.deltaTime is different between Unity 2018 and Unity 2019 as shown below.
The top gif is Unity 2018 and the bottom gif is the execution result of Unity 2019.

<img width="400" alt="Unity2018_4_36f1" src="https://user-images.githubusercontent.com/29646672/137258668-5bc8da69-2273-4548-b582-cc5789d6e670.gif">
<img width="400" alt="Unity2019_4_28f1" src="https://user-images.githubusercontent.com/29646672/135413141-22dbdf65-506c-4920-8268-90977f7ba4e3.gif">


It seems that the average is 16 [msec] = 60 [FPS]

※(If you see the graph above, it has a sawtooth wave. This tells that there has been correction made from Unity 2019 and onward regarding the accuracy of Time.deltaTime)

On a contrary, the difference (green graph) from the frame rate before Time.timeSinceStartup is constant. This indicates that there were no correction being made.


