# Getting started with AI and ML coding
In this lesson, we'll cover the basic software tools:

* Jupyter notebooks
* Numpy, matplotlib (also: tensors!)
* Tensorflow
* ml5js

Make sure you have your software environment ready, either using a school computer with everything preinstalled, or installing the various packages on your computer, as instructed in [the course prerequisites](Prerequisites.md).

**The exercises of this lesson are in the form of Jupyter notebooks, except for ml5js. Each notebook ends with suggested modifications; implementing them verifies your learning.** We have provided model solutions in separate notebooks with the "solutions" suffix, in the same folder.

After you've checked the notebooks and tried to implement the modifications, review the key takeaways listed below.

## Jupyter notebooks
Jupyter notebooks are a standard pedagogical tool in machine learning. Once you have a Jupyter server running on your local machine or remotely, you can write and run Python code in a browser. This has the following benefits:

* You can execute and edit code in small snippets, which can save time because you don't have to restart the whole program

* Tutorial explanations and figures can be interleaved with code

* The notebooks (.ipynb files) can be easily shared on Github, which renders them in the browser, including generated images. Note that these rendered versions are static, without any interactive features.

On the other hand, the Jupyter interface lacks code autocompletion and other common productivity features, which is why most serious developers use tools like Visual Studio instead of Jupyter.

# Running the notebooks
You can run the notebooks locally on your machine by opening the Anaconda prompt and typing ```activate MediaAI``` (assuming that MediaAI is the Anaconda environment into which you've installed everything), and then ```jupyter notebook --notebook-dir MYPATH```, where the you should replace MYPATH with the folder where you've copied or cloned this repository. Alternatively, you can log in to [Aalto's Jupyter service](https://jupyter.cs.aalto.fi) with your Aalto account, and select this course from the spawner menu. After this, when you do this for the first time, clone this repository by opening a terminal and typing ```git clone https://github.com/PerttuHamalainen/MediaAI```. After that, you can browse to the code/Jupyter folder and click on the notebooks to open and edit. 

## Tensors, numpy, matplotlib
[This Jupyter notebook](../Code/Jupyter/DataAndTensors.ipynb) introduces you to tensors, numpy and matplotlib, the basic tools you need for any scientific computing.

Key takeaways:

* Tensors are multidimensional arrays of numbers. The *rank* of a tensor defines the dimensionality.
* The *shape* of a tensor is an 1D array of numbers that describe how many elements the array has in each dimension.
* In machine learning, data is typically processed in (mini)batches. Thus, even if the data is 1D, the tensors passed to a neural network are 2D. In image processing, we typically have 4D tensors with shape [nBatch,height,width,nChannels]. In audio, we have the same if the data is encoded as spectograms. For raw waveforms, one typically uses [nBatch,nSamples,nChannels].
* Tensor math operations like division or multiplication are performed elementwise. If you want to use linear algebra operations like a dot product in Numpy, you must explicitly type np.dot(tensor_a,tensor_b).
* Numpy code often utilizes *broadcasting*. Broadcasting expands tensors with only 1 element such that tensor math operations are possible to compute. For example, one can multiply together tensors of shape [4,4] and [4,1]; the latter is expanded to a [4,4] as if it was stacked 4 times along the second dimension. However, shapes [4,4] and [4] cannot be multiplied together, even though tensors of shapes [4] and [4,1] can contain exactly the same data! If you want to perform such multiplication, you must first explicitly reshape the [4] tensor to a [4,1] using np.reshape()  

## Tensorflow
[Tensorflow](https://www.tensorflow.org/) is the currently dominant deep learning library, although [PyTorch](https://pytorch.org/) is gaining in popularity.

[This notebook](../Code/Jupyter/PredictWeight.ipynb) trains a single neuron neural network using a [Kaggle](https://www.kaggle.com/) dataset of human height and weight.

[This notebook](../Code/Jupyter/MNIST.ipynb) continues the lesson into image classification, the bread-and-butter of neural networks. As an exercise, you're asked to modify the network for classifying audio and visualize/sonify the learned features.

The key takeaways:

* Tensorflow works like Numpy, but by constructing a *compute graph* of the math operations, as illustrated in the figure below.
* *Variables* are special tensors that do not just represent the results of tensor operations. They fully define the state of the compute graph; saving or loading a trained neural network only requires saving or loading the variables.
* A core operation supported by Tensorflow is optimization: adjusting variables to minimize some loss function. Training a neural network is just that: adjusting the variables that represent the neural network parameters to minimize model error. *We'll talk more about optimization later on the course.*
* The compute graph can reside on the GPU; to enable good performance, Tensorflow allows the user to manage the traffic between the host program and the graph. This is why you use sess.run() to both send data to the graph and get results back. **One can't simply get or set the contents of a Tensorflow tensor or variable**.

*If one wants to not care about the compute graph, which can be a bit cumbersome, one can use Pytorch or the eager execution mode of Tensorflow. These make the programming style closer to Numpy, but it's easy to write code that doesn't run optimally fast or allocates more memory than really needed.*

**TODO: add compute graph figure**

## ml5js, Tensorflow.js
A recent alternative to Python is Javascript, using the [p5js](http://p5js.org/) and [ml5js](https://ml5js.org/) libraries. p5js is a Javascript Processing-inspired audiovisual coding framework, and m5js is a high-level wrapper for [Tensorflow.js](https://www.tensorflow.org/js). The [p5js web editor](https://editor.p5js.org/) allows you to start writing machine learning & Processing code in the browser, without installing anything. You can also [embed your scripts in an html file](https://github.com/tensorflow/tfjs-examples/tree/master/mnist), which however requires some additional code.

[This p5js demo]() **TODO** does the same as the human height & weight Jupyter notebook above. It should illustrate the similarities and differences between Python and Javascript.

On this course, students can choose whatever software tools and frameworks they prefer. If you are more experienced in Javascript than Python, it might be a good choice. In particular, p5js provides easy [interactive visualization](https://p5js.org/examples/) and a quite a wide palette of [sound synthesis tools](https://p5js.org/reference/#/libraries/p5.sound).

*Caution:* Getting your data into the Javascript environment can be tricky. For example, in this [MNIST example](https://github.com/tensorflow/tfjs-examples/tree/master/mnist), the whole dataset was encoded as a huge PNG image that was then loaded as a p5js sprite.
