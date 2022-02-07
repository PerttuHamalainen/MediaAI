# Prerequisites
Before you attend the class, you should **go through the materials and instructions on this page**. This should take about 2 hours, plus an extra 1-2 hours if you install all the software on your computer.

**Do the following**

* Go through the selection of Two Minute Papers videos below to get an idea of the range of computational media applications possible with modern machine learning.

* To grasp the fundamentals of what neural networks are doing, watch episodes 1-3 of 3Blue1Brown's [neural network series](https://www.youtube.com/playlist?list=PLZHQObOWTQDNU6R1_67000Dx_ZCJB-3pi)

* Learn about Colab notebooks (used for course exercises) by watching these videos: [Video 1](https://www.youtube.com/watch?v=inN8seMm7UI), [Video 2](https://www.youtube.com/watch?v=PitcORQSjNM) and reading through this [Tutorial notebook](https://colab.research.google.com/notebooks/welcome.ipynb). Feel free to also take a peek at this course's exercise notebooks such as [GAN image generation](https://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/BigGAN%20test.ipynb). To test the notebook, **select "run all"** from the **"runtime" menu**. This runs all the code cells in sequence and should generate and display some images at the bottom of the notebook. In general, when opening a notebook, it's good to use "run all" first, before starting to modify individual code cells, because the cells often depend on variables initialized or packages imported in the preceding cells.

* Prepare to add one slide to a shared Google Slides document (link provided at the first lecture), including 1) your name and photo, 2) your background and skillset, 3) and what kind of projects you want to work on. This will be useful for finding other students with similar interests and/or complementary skills.

* Optional: Learn Python basics, e.g., using [these resources](Python.md). You should be able to pick things up as we go, especially if you already know some other programming language, but some Python experience definitely makes things easier.

* Optional: Install the software tools as instructed at the bottom of this page.


**A selection Two Minute Papers videos:**

Game playing:
* [Playing Atari games with deep reinforcement learning](https://www.youtube.com/watch?v=Ih8EfvOzBOY&index=3&t=11s&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2)
* [Game playing with artificial curiosity](https://www.youtube.com/watch?v=fzuYEStsQxc&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&index=176&t=0s)
* [AlphaZero](https://www.youtube.com/watch?v=2ciR6rA85tg&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&index=107&t=0s)

Image processing and synthesis:
* [Universal Style Transfer](https://www.youtube.com/watch?v=v1oWke0Qf1E&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&t=0s&index=104)
* [Learning to see in the dark](https://www.youtube.com/watch?v=bcZFQ3f26pA&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&t=0s&index=144)
* [Image matting](https://www.youtube.com/watch?v=6DVng5JVuhI&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&index=100&t=0s)
* [Generating images from drawings (pix2pix)](https://www.youtube.com/watch?v=u7kQ5lNfUfg)
* [Everybody Dance Now](https://www.youtube.com/watch?v=cEBgi6QYDhQ&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&index=161&t=0s)
* [Google's Big GAN](https://www.youtube.com/watch?v=ZKQp28OqwNQ&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&t=0s&index=182) A pretrained BigGAN is what powers many recent GAN art projects, e.g., [GanBreeder](https://ganbreeder.app/)
* [GAN dissection](https://www.youtube.com/watch?v=iM4PPGDQry0)
* [Generating images from text descriptions](https://www.youtube.com/watch?v=9bcbh2hC7Hw&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&index=69&t=0s)

Processing and synthesis of other media:
* [WaveNet](https://www.youtube.com/watch?v=CqFIVCD1WWo&index=42&t=0s&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2)
* [Google's text to speech](https://www.youtube.com/watch?v=bdM9c2OFYuw&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&t=0s&index=119)
* [Singing synthesis](https://www.youtube.com/watch?v=HANeLG0l2GA&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&index=121&t=0s)
* [Recurrent neural network writes music and novels](https://www.youtube.com/watch?v=Jkkjy7dVdaY&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&index=8&t=0s)
* [Terrain generation](https://www.youtube.com/watch?v=NEscK5RCtlo&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&index=99&t=0s)
* [Hallucinating audio based on video](https://www.youtube.com/watch?v=flOevlA9RyQ)


## Software installation
**Option 1 (recommended): Don't install anything, just use Google Colab**: [Google Colab](https://colab.research.google.com) is a browser-based environment for developing and running machine learning code as [Jupyter Notebooks](https://jupyter.org/), which contain code interspersed with text explanations. All the Python exercises and tutorials of this course are available through Colab's Github integration: http://colab.research.google.com/github/PerttuHamalainen/MediaAI.

Note: Colab let's you access Google's GPU:s and TPU:s for faster neural network training, but those are not always available. [Colab Pro](https://colab.research.google.com/signup) ensures access to better GPU:s with a modest monthly fee, and is a good option for AI and machine learning hobbyists. However, all the course exercises are designed to work on the hardware available in the free version of Colab.  

**Option 2: I want to set up my own development environment:** If you plan to work on your own computer, you should install a number of software packages. On Windows, it's recommended to install the Linux versions of Anaconda, Tensorflow and Pytorch through the Windows Subsystem for Linux.

* [Microsoft Visual Studio](https://visualstudio.microsoft.com/vs/community/) or [Visual Studio Code](https://code.visualstudio.com/) with Python support (check the install options). While many develop Python code with other IDEs like Spyder or Pycharm, Visual Studio is the recommended IDE for this course because it also works for any game AI experiments done in Unity C#.
* [Anaconda](https://www.anaconda.com/distribution/) with Python 3.x. If you're using Windows, it's recommended to set the environment variable CONDA_ENVS_PATH to c:\CondaEnvs\ or something equally short; the default path is so long that installing some python packages like OpenAI Gym with MuJoCo will fail. After installing Anaconda, open the Anaconda command prompt and create a new virtual environment into which you'll install everything else by typing ```conda create MediaAI``` and then ```activate MediaAI```. This is important because if you want to use some open source deep learning code, chances are it will require different versions of some Python package. Using separate virtual environments for different projects allows you to have multiple versions of the packages.
* [Tensorflow](https://www.tensorflow.org/), preferably with GPU support, which however requires some extra prerequisites like CUDA.  **A note on Tensorflow versions:** In version 2, the Tensorflow API has changed significantly. Some of the Jupyter Notebooks of this course use Tensorflow 1 and version 1.15 is recommended for them.  
* [Pillow](https://pillow.readthedocs.io/en/stable/). This is a Python package that helps in loading and saving images. Use ```pip install Pillow``` in the Anaconda prompt, with your desired virtual environment activated.
* [PyTorch](https://pytorch.org/). PyTorch is an alternative to Tensorflow, backed by Facebook Research.
* [Jupyter](https://jupyter.org/). Jupyter is already included as part of Anaconda, but some extra tricks are needed to make it work with Anaconda virtual envs. In your Anaconda command prompt, with your MediaAI environment activated, type ```pip install ipykernel``` and then ```python -m ipykernel install --user --name=MediaAI```. Now, you should be able to run a Jupyter notebook test as ```jupyter notebook <notebookpath>```, where you replace <notebookpath> with, e.g., [this file](MyFirstMachineLearningModel.ipynb) from the code folder of this repository. (NOTE: If you click on the link, Github will render a static version of the notebook that you cannot edit.)
* [Unity](https://unity.com/), the world's most popular game engine, which we use for game AI
* [Unity Machine Learning Agents](https://github.com/Unity-Technologies/ml-agents), Unity's framework for training deep reinforcement learning Agents

**How to reinstall if something is broken**
Occasionally, you might get weird errors such as a missing dll at the ```import tensorflow as tf```statement at the start of a python file. In this case, you can try forcing a reinstall. Open Anaconda prompt, type ```activate MediaAI``` and then ```pip install --force-reinstall tensorflow```.

**Important note about Unity ML Agents:**

ML agents installation instructions are here https://github.com/Unity-Technologies/ml-agents/blob/latest_release/docs/Installation.md. However, note that ML agents may require a specific version of Tensorflow or Pytorch, which is why you should definitely use a dedicated Python virtual environment for it as follows, before the ```pip3 install mlagents``` step of the ML agents installation procedure.
