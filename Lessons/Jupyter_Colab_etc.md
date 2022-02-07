<!--Make sure you have your software environment ready, if you want to develop on  either using a school computer with everything preinstalled, or installing the various packages on your computer, as instructed in [the course prerequisites](Prerequisites.md).-->

## About Jupyter notebooks
**Most of the exercises of this course are in the form of Jupyter notebooks**. However, see below about [ml5js](https://ml5js.org/).

Jupyter notebooks are a standard pedagogical tool in machine learning. They allow you to write and run Python code in a browser. This has the following benefits:

* You can execute and edit code in small snippets, which can save time because you don't have to restart the whole program

* Tutorial explanations and figures can be interleaved with code

* The notebooks (.ipynb files) can be easily shared on Google Colab (a Google Drive add-on) like other Google docs.

On the other hand, Jupyter may become cumbersome for larger projects, in which case most developers use Integrated Development Environments (IDE:s) like Visual Studio or Pycharm.

## How to run the Notebooks
Students may install Anaconda and Jupyter on their computers and run the notebooks locally, but the recommended option is to run the notebooks on Google's Colab service, by clicking on the links in the course [syllabus](../README.md).

If you haven't used Colab before, check out this tutorial: https://colab.research.google.com/notebooks/welcome.ipynb

## ml5js, Tensorflow.js
A recent alternative to Python is Javascript, using the [p5js](http://p5js.org/) and [ml5js](https://ml5js.org/) libraries. p5js is a Javascript Processing-inspired audiovisual coding framework, and m5js is a high-level wrapper for [Tensorflow.js](https://www.tensorflow.org/js). The [p5js web editor](https://editor.p5js.org/) allows you to start writing machine learning & Processing code in the browser, without installing anything. You can also [embed your scripts in an html file](https://github.com/tensorflow/tfjs-examples/tree/master/mnist), which however requires some additional code.

<!-- [This p5js demo]() **TODO:** does the same as the human height & weight Jupyter notebook above. It should illustrate the similarities and differences between Python and Javascript.-->

On this course, students can choose whatever software tools and frameworks they prefer. If you are more experienced in Javascript than Python, it might be a good choice. In particular, p5js provides easy [interactive visualization](https://p5js.org/examples/) and a quite a wide palette of [sound synthesis tools](https://p5js.org/reference/#/libraries/p5.sound).

*Caution:* Getting your data into the Javascript environment can be tricky. For example, in this [MNIST example](https://github.com/tensorflow/tfjs-examples/tree/master/mnist), the whole dataset was encoded as a huge PNG image that was then loaded as a p5js sprite.
