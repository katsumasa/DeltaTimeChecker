# DeltaTimeChecker

Time.deltaTimeの値をグラフで視覚的に確認するプログラムです。

VSYNCカウントで同期を行わず、Application.targetFrameRateでフレームレートを設定する場合、、targetFrameRateには、-1、画面のリフレッシュレートに等しい値、またはリフレッシュレートを整数で割った値のいずれかを設定する必要があります。
例えば、デバイスのリフレッシュレートが90[Hz]、Application.targetFrameRate=60とした場合、Time.deltaTimeの値(黄色のグラフ)は0.01~22[msec]の間で鋸歯のような波形を繰り返します。

![734f4d3a36fdebe8e79f3707299182d9](https://user-images.githubusercontent.com/29646672/135413141-22dbdf65-506c-4920-8268-90977f7ba4e3.gif)

平均で16[msec] = 60[FPS]となっている所がいじらしい。

※(鋸歯になるのはUnity2019以降である為、Unity2019以降で、Time.deltaTimeをより正確にする補正が入ったと考えられます。)

一方、Time.timeSinceStartup の前のフレームレートとの差分(緑のグラフ)は一定の為、こちらには補正が入っていないということになります。

