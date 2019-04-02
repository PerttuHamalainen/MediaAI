# Prerequisites
Before you attend the class, you should go through the following (about 2 hours total):

* Go through the selection of Two Minute Papers videos below to get an idea of the range of computational media applications possible with modern machine learning.

* To grasp the fundamentals of what neural networks are doing, watch episodes 1-3 of 3Blue1Brown's [neural network series](https://www.youtube.com/playlist?list=PLZHQObOWTQDNU6R1_67000Dx_ZCJB-3pi)

* Prepare to add one slide to a shared Google Slides document (link provided at the first lecture) about your background and what kind of projects you want to work on. This will be useful for finding other students with similar interests and/or complementary skills.

**A selection Two Minute Papers videos:**

Game playing:
* [Playing Atari games with deep reinforcement learning](https://www.youtube.com/watch?v=Ih8EfvOzBOY&index=3&t=11s&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2)
* [Game playing with artificial curiosity](https://www.youtube.com/watch?v=fzuYEStsQxc&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&index=176&t=0s)
* [AlphaZero](https://www.youtube.com/watch?v=2ciR6rA85tg&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&index=107&t=0s)

Image processing and synthesis:
* [Universal Style Transfer](https://www.youtube.com/watch?v=v1oWke0Qf1E&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&t=0s&index=104)
* [Learning to see in the dark](https://www.youtube.com/watch?v=bcZFQ3f26pA&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&t=0s&index=144)
* [Image matting](https://www.youtube.com/watch?v=6DVng5JVuhI&list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2&index=100&t=0s)
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

## Software installation
If you plan to work on your own computer, you should install the software packages below. These will be pre-installed on the classroom computers, on which students can't install anything on their own.

* [Microsoft Visual Studio](https://visualstudio.microsoft.com/vs/community/) with Python tools (check the install options). While many develop Python code with other IDEs like Spyder or Pycharm, Visual Studio is the recommended IDE for this course because it also works for any game AI experiments done in Unity C#.
* [Anaconda](https://www.anaconda.com/distribution/) with Python 3.x. If you're using Windows, it's recommended to set the environment variable CONDA_ENVS_PATH to c:\CondaEnvs\ or something equally short; the default path is so long that installing some python packages like OpenAI Gym with MuJoCo will fail. After installing Anaconda, open the Anaconda command prompt and create a new virtual environment into which you'll install everything else by typing "conda create MediaAI" and then "activate MediaAI".
* [Tensorflow](https://www.tensorflow.org/), preferably with GPU support, which however requires some extra prerequisites like CUDA. With the prerequisites in place, you should be able to install Tensorflow by typing "pip install --ignore-installed --upgrade tensorflow-gpu" in the Anaconda prompt, with your MediaAI virtual environment. If you find the GPU support  installation too cumbersome or don't have at least an NVIDIA GeForce GTX 1050 or better graphics card, you can install the CPU version using "pip install --ignore-installed --upgrade tensorflow". However, this will train large neural networks much more slowly. For small networks and first experiments, the CPU version works fine.  
* [Pillow](https://pillow.readthedocs.io/en/stable/). This is a Python package that helps in loading and saving images. Use "pip install Pillow" in the Anaconda prompt, with your MediaAI virtual environment.
* [Unity](https://unity.com/), the world's most popular game engine, which we use for game AI
* [Unity Machine Learning Agents](https://github.com/Unity-Technologies/ml-agents), Unity's framework for training deep reinforcement learning Agents
