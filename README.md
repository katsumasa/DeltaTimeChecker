# DeltaTimeChecker

Time.deltaTimeの値をグラフで視覚的に確認するプログラムです。

VSYNCカウントで同期を行わず、Application.targetFrameRateでフレームレートを設定する場合、targetFrameRateには、-1、画面のリフレッシュレートに等しい値、またはリフレッシュレートを整数で割った値のいずれかを設定する必要があります。



デバイスのリフレッシュレートが90[Hz]、Application.targetFrameRate=60とした場合、Unity2018とUnity2019では下記のようにTime.deltaTimeの値が異なっています。
上がUnity2018で下がUnity2019の実行結果です。

<img width="400" alt="Unity2018_4_36f1" src="https://user-images.githubusercontent.com/29646672/137258668-5bc8da69-2273-4548-b582-cc5789d6e670.gif">
<img width="400" alt="Unity2019_4_28f1" src="https://user-images.githubusercontent.com/29646672/135413141-22dbdf65-506c-4920-8268-90977f7ba4e3.gif">


※平均では16[msec] = 60[FPS]となっているようですが・・・。

※(鋸歯になるのはUnity2019以降である為、Unity2019以降で、Time.deltaTimeをより正確にする補正が入ったと考えられます。)

一方、Time.timeSinceStartup の前のフレームレートとの差分(緑のグラフ)は一定の為、こちらには補正が入っていないということになります。


