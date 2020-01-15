# Prerequisites
Before you attend the class, you should go through the materials and instructions on this page. This will take about 2 hours, plus an extra 1-2 hours if you install all the software on your computer. *Software installation instructions are at the bottom of this page.*

**Do the following**

* Go through the selection of Two Minute Papers videos below to get an idea of the range of computational media applications possible with modern machine learning.

* To grasp the fundamentals of what neural networks are doing, watch episodes 1-3 of 3Blue1Brown's [neural network series](https://www.youtube.com/playlist?list=PLZHQObOWTQDNU6R1_67000Dx_ZCJB-3pi)

* Prepare to add one slide to a shared Google Slides document (link provided at the first lecture), including 1) your name and photo, 2) your background and skillset, 3) and what kind of projects you want to work on. This will be useful for finding other students with similar interests and/or complementary skills.

* Optional: Install the software tools as instructed at the bottom of this page

* Optional: Check out the awesome [Elements of AI](https://www.elementsofai.com/) course by University of Helsinki and Reaktor.


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
If you plan to work on your own computer, you should install the software packages below. These will be pre-installed on the classroom computers, on which students can't install anything on their own. NOTE: You can work on the Jupyter exercises on your own computer without installing anything, as they are also available through Aalto's browser-based Jupyter service.

* [Microsoft Visual Studio](https://visualstudio.microsoft.com/vs/community/) or [Visual Studio Code](https://code.visualstudio.com/) with Python support (check the install options). While many develop Python code with other IDEs like Spyder or Pycharm, Visual Studio is the recommended IDE for this course because it also works for any game AI experiments done in Unity C#.
* [Anaconda](https://www.anaconda.com/distribution/) with Python 3.x. If you're using Windows, it's recommended to set the environment variable CONDA_ENVS_PATH to c:\CondaEnvs\ or something equally short; the default path is so long that installing some python packages like OpenAI Gym with MuJoCo will fail. After installing Anaconda, open the Anaconda command prompt and create a new virtual environment into which you'll install everything else by typing ```conda create --name MediaAI``` and then ```activate MediaAI```. This is important because if you want to use some open source deep learning code, chances are it will require different versions of some Python package. Using separate virtual environments for different projects allows you to have multiple versions of the packages.
* [Tensorflow](https://www.tensorflow.org/), preferably with GPU support, which however requires some extra prerequisites like CUDA. With the prerequisites in place, you should be able to install Tensorflow by typing "pip install --ignore-installed --upgrade tensorflow-gpu" in the Anaconda prompt, with your MediaAI virtual environment. If you find the GPU support  installation too cumbersome or don't have at least an NVIDIA GeForce GTX 1050 or better graphics card, you can install the CPU version using ```pip install --ignore-installed --upgrade tensorflow```. However, this will train large neural networks much more slowly. For small networks and first experiments, the CPU version works fine.  
* [Pillow](https://pillow.readthedocs.io/en/stable/). This is a Python package that helps in loading and saving images. Use ```pip install Pillow``` in the Anaconda prompt, with your MediaAI virtual environment activated.
* [PyTorch](https://pytorch.org/) (optional). Although most of the exercises and a majority of open source AI & ML code use Tensorflow, PyTorch is a competing deep learning framework that has been gaining in popularity and is backed by Facebook research. PyTorch can be more beginner-friendly, which is why we include some examples of how the same things can be achieved using both Tensorflow and PyTorch.
* [Jupyter](https://jupyter.org/). Jupyter is already included as part of Anaconda, but some extra tricks are needed to make it work with Anaconda virtual envs. In your Anaconda command prompt, with the MediaAI environment activated, type ```pip install ipykernel``` and then ```python -m ipykernel install --user --name=MediaAI```. Now, you should be able to run a Jupyter notebook test as ```jupyter notebook <notebookpath>```, where you replace <notebookpath> with, e.g., [this file](MyFirstMachineLearningModel.ipynb) from the code folder of this repository. (NOTE: If you click on the link, Github will render a static version of the notebook that you cannot edit.)
* [Unity](https://unity.com/), the world's most popular game engine, which we use for game AI
* [Unity Machine Learning Agents](https://github.com/Unity-Technologies/ml-agents), Unity's framework for training deep reinforcement learning Agents

**How to reinstall if something is broken**
Occasionally, you might get weird errors such as a missing dll at the ```import tensorflow as tf```statement at the start of a python file. In this case, you can try forcing a reinstall. Open Anaconda prompt, type ```activate MediaAI``` and then ```pip install --force-reinstall tensorflow```.

**Important note about Unity ML Agents:**

ML agents currently requires an older version of Tensorflow. Thus, you need to create a new Anaconda virtual environment for it and install Tensorflow again. Fortunately, Unity ML installs it for you together with the other dependencies, if you do the following:

1.	Open Anaconda prompt
2.	Type ```conda create –n ml-agents python=3.6```. This creates an Anaconda environment, allowing you to install the specific Tensorflow and other package versions that Unity ML needs, without breaking your other projects. Note: if you are at an Aalto classroom Windows computer, you should instead use ```conda create –-prefix z:\ml-agents python=3.6``` because you can only install things on your own network folder (z:).
3.	Type ```activate ml-agents``` or ```activate z:\ml-agents```, depending on what you did above.
4.	go to a suitable folder, e.g., your Aalto home drive on the school computers ("z:")
5.	Type ```git clone https://github.com/Unity-Technologies/ml-agents.git```
6.	Type ```cd ml-agents-master/ml-agents```
7.	Type ```pip install -e .```
8.  Open the 3DBall scene in Unity, found in the ```unity-environment\Assets\ML-Agents\Examples\3DBall``` folder of the ML Agents repository that you cloned above.
9.  Now, as explained in [Unity ML docs](https://github.com/Unity-Technologies/ml-agents/blob/master/docs/Training-ML-Agents.md), you should be able to type ```mlagents-learn 3DBall_trained``` at your Anaconda prompt and press play in Unity when you get the "Start training by pressing the Play button in the Unity Editor" message. 
