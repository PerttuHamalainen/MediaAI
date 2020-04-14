# Getting started with AI and ML coding
In this lesson, we'll cover the basic software tools:

* Jupyter notebooks & Google Colab
* Numpy, Pandas, Matplotlib (also: tensors!)
* Tensorflow
* ml5js

<!--Make sure you have your software environment ready, if you want to develop on  either using a school computer with everything preinstalled, or installing the various packages on your computer, as instructed in [the course prerequisites](Prerequisites.md).-->

## About Jupyter notebooks
**The exercises of this lesson are in the form of Jupyter notebooks (see below for more), except for ml5js.**

Jupyter notebooks are a standard pedagogical tool in machine learning. They allow you to write and run Python code in a browser. This has the following benefits:

* You can execute and edit code in small snippets, which can save time because you don't have to restart the whole program

* Tutorial explanations and figures can be interleaved with code

* The notebooks (.ipynb files) can be easily shared on Google Colab (a Google Drive add-on) like other Google docs.

On the other hand, Jupyter may become cumbersome for larger projects, in which case most developers use Integrated Development Environments (IDE:s) like Visual Studio or Pycharm.

## How to run the Notebooks
There are 3 main ways to run the notebooks:

* **Hosted by Google Colab:** Just click on the links in the next section. *This is the currently recommended option.*

* **Hosted by Aalto:** log in to [Aalto's Jupyter service](https://jupyter.cs.aalto.fi) with your Aalto account, and select this course from the spawner menu. After this, when you do this for the first time, clone this repository by opening a terminal and typing ```git clone https://github.com/PerttuHamalainen/MediaAI```. After that, you can browse to the code/Jupyter folder and click on the notebooks to open and edit.

* **On your own computer:** Follow the software installation instructions here: https://github.com/PerttuHamalainen/MediaAI/blob/master/Lessons/GettingStarted.md You can run the notebooks locally on your machine by opening the Anaconda prompt and typing ```activate MediaAI``` (assuming that MediaAI is the Anaconda environment into which you've installed everything), and then ```jupyter notebook --notebook-dir MYPATH```, where the you should replace MYPATH with the folder where you've copied or cloned this repository.


## Lesson structure
The lesson consists of a progression of Jupyter notebooks. Each notebook ends with suggested modifications; implementing them verifies your learning. If you get stuck, you can take a peek at the model solutions.

* Introduction to tensors, numpy and matplotlib, the basic tools you need for any scientific computing. [Open in Colab](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/DataAndTensors.ipynb), [Solutions](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/DataAndTensors_solutions.ipynb).

* Training a very simple neural network using a [Kaggle](https://www.kaggle.com/) dataset of human height and weight. [Open in Colab](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/PredictWeight.ipynb), [Solutions](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/PredictWeight_solutions.ipynb)

* Image classification, the bread-and-butter of neural networks. [Open in Colab](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/MNIST.ipynb), [Solutions](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/MNIST_solutions.ipynb)

* Fooling the image classifier with adversarial images.  This is a bit more advanced topic, but included to demonstrate that discriminative models also have applications in generating images and visualizations. We will revisit the topic later. [Open in Colab](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/AdversarialMNIST.ipynb). TODO: provide solutions.

* Sound classification, to illustrate how processing audio can be done very similarly to images. [Open in Colab](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/AudioMNIST.ipynb). TODO: Provide a solution to the exercise.  


The lesson focuses on [Tensorflow](https://www.tensorflow.org/), which is the currently dominant deep learning library, although [PyTorch](https://pytorch.org/) is gaining in popularity. Those interested are encouraged to port the exercises to PyTorch!

After you've checked the notebooks and tried to implement the modifications, review the key takeaways below.


## Tensors, numpy, matplotlib key takeaways

* Tensors are multidimensional arrays of numbers. The *rank* of a tensor defines the dimensionality.
* The *shape* of a tensor is an 1D array of numbers that describe how many elements the array has in each dimension.
* In machine learning, data is typically processed in (mini)batches. Thus, even if the data is 1D, the tensors passed to a neural network are 2D. In image processing, we typically have 4D tensors with shape [nBatch,height,width,nChannels]. In audio, we have the same if the data is encoded as spectograms. For raw waveforms, one typically uses [nBatch,nSamples,nChannels].
* Tensor math operations like division or multiplication are performed elementwise. If you want to use linear algebra operations like a dot product in Numpy, you must explicitly type np.dot(tensor_a,tensor_b).
* Numpy code often utilizes *broadcasting*. Broadcasting expands tensors with only 1 element such that tensor math operations are possible to compute. For example, one can multiply together tensors of shape [4,4] and [4,1]; the latter is expanded to a [4,4] as if it was stacked 4 times along the second dimension. However, shapes [4,4] and [4] cannot be multiplied together, even though tensors of shapes [4] and [4,1] can contain exactly the same data! If you want to perform such multiplication, you must first explicitly reshape the [4] tensor to a [4,1] using np.reshape()  

## Tensorflow key takeaways

* Tensorflow works like Numpy, but by constructing a *compute graph* of the math operations, as illustrated in the figure below.
* *Variables* are special tensors that do not just represent the results of tensor operations. They fully define the state of the compute graph; saving or loading a trained neural network only requires saving or loading the variables.
* A core operation supported by Tensorflow is optimization: adjusting variables to minimize some loss function. Training a neural network is just that: adjusting the variables that represent the neural network parameters to minimize model error. *We'll talk more about optimization later on the course.*
* The compute graph can reside on the GPU; to enable good performance, Tensorflow allows the user to manage the traffic between the host program and the graph. This is why you use sess.run() to both send data to the graph and get results back. **One can't simply get or set the contents of a Tensorflow tensor or variable**.

*If one wants to not care about the compute graph, which can be a bit cumbersome, one can use Pytorch or the eager execution mode of Tensorflow. These make the programming style closer to Numpy, but it's easy to write code that doesn't run optimally fast or allocates more memory than really needed.*

**TODO: add compute graph figure**

## ml5js, Tensorflow.js
A recent alternative to Python is Javascript, using the [p5js](http://p5js.org/) and [ml5js](https://ml5js.org/) libraries. p5js is a Javascript Processing-inspired audiovisual coding framework, and m5js is a high-level wrapper for [Tensorflow.js](https://www.tensorflow.org/js). The [p5js web editor](https://editor.p5js.org/) allows you to start writing machine learning & Processing code in the browser, without installing anything. You can also [embed your scripts in an html file](https://github.com/tensorflow/tfjs-examples/tree/master/mnist), which however requires some additional code.

<!-- [This p5js demo]() **TODO:** does the same as the human height & weight Jupyter notebook above. It should illustrate the similarities and differences between Python and Javascript.-->

On this course, students can choose whatever software tools and frameworks they prefer. If you are more experienced in Javascript than Python, it might be a good choice. In particular, p5js provides easy [interactive visualization](https://p5js.org/examples/) and a quite a wide palette of [sound synthesis tools](https://p5js.org/reference/#/libraries/p5.sound).

*Caution:* Getting your data into the Javascript environment can be tricky. For example, in this [MNIST example](https://github.com/tensorflow/tfjs-examples/tree/master/mnist), the whole dataset was encoded as a huge PNG image that was then loaded as a p5js sprite.
