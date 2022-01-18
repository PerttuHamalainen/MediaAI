# AI for Media, Art and Design (A.k.a. "Intelligent Computational Media")

<!--
![Some examples of content covered](Lessons/Media/github_preview.jpg)
-->
![Images created with the CLIP (Contrastive Language-Image Pre-training; Radford et al., 2021) model](Lessons/Media/clip_256_wide.jpg)

*Images created with the CLIP (Contrastive Language-Image Pre-training; Radford et al., 2021) model*

This repository contains the lectures and materials of Aalto University's Intelligent Computational Media course.

Follow the course's [Twitter feed](https://twitter.com/aaltomediaai) for links and resources.

**2022 Coronavirus note:** we wait for further instructions but at present, the course will likely be held in hybrid mode or fully online (Zoom).

## Course Overview & Design Philosophy

This is a hands-on, project-based crash course for deep learning and other AI techniques for people with **as little technical prerequisites as possible.** The focus is on **media processing and games**, which makes this particularly suitable for **artists and designers**. 

The 2022 edition of this course will be taught over a **three week period** from 01.02.-18.02.2022 by **Prof Perttu Hämäläinen** ([Twitter](https://twitter.com/perttu_h)), **Dr Christian Guckelsberger** ([Twitter](https://twitter.com/creativeEndvs)) and **Nam Hee Gordon Kim.** ([Twitter](https://twitter.com/NamHeeGordonKim)). Registeration through [Aalto's Mycourses system](https://mycourses.aalto.fi), specific inquiries to: perttu.hamalainen[at]aalto.fi (but check the info below first). 

### Materials

The course is taught through:

- Lectures
- Software examples
- Exercises that require you to modify the software examples to test your learning. We always try to provide both easy and advanced exercises to cater for different skill levels. Model solutions are provided for the easier ones, but some of the advanced exercises are left to the students to accomplish and can also become the final project.

This is accompanied with individual and repeating **one-on-one tutoring sessions** that students will be invited to. Outside lectures and exercises, students can reach course staff through **Discord** (invitations will be send to registered students). 

### Learning Goals

The goal is for students to:

- understand how common AI algorithms & tools work,
- understand what the tools can be used for in context of art, media, and design, and
- get hands-on practice of designing, implementing and/or using the tools.

### Student Prerequisites

- Basic programming skills (e.g., some python, processing, or Unity C#)
- Basic high-school math. We focus on visual explanations that are enough to understand why and how various AI methods work.

### Grading / Project Work

The lectures and exercises should only take about half of the time, and there's no need to complete all the exercises. The rest of the time is for project work that students can do either alone or in groups. You **pass the course** by submitting a report of the project (details in the lecture slides). 

Grading is **primarily based on the student projects** which should demonstrate personal learning/development and be clearly documented  using images, video, and short textual description. The project documentation is **to be submitted through [MyCourses](http://mycourses.aalto.fi)**. It is also recommended to make the project publicly available, e.g., as a Github repository. You may simply submit a link to the repository, if the repository contains the needed documentation.

Students can choose their **project topics** based on their own interests and learning goals. Projects are **agreed on** with the lecturers in individual tutoring sessions. You could create something in Colab or Unity Machine Learning agents, or if you'd rather not write any code, experimenting with artist-friendly tools like [GIMP-ML](https://github.com/kritiksoman/GIMP-ML) or [RunwayML](https://runwayml.com/) to create something new and/or interesting is also ok. For example, one could generate song lyrics using [https://theselyricsdonotexist.com/](https://theselyricsdonotexist.com/), and use them to compose and record a song.

### Comparison to other courses

Gene Kogan's NYU course [Neural Aesthetics](https://ml4a.github.io/classes/itp-F18/):

- This course covers similar content as Kogan’s excellent course, but run over 3 weeks with intensive sessions from 9am to 12am, Tuesday-Friday, according to Aalto Media Lab's usual study schedule. This is thus more of a short crash course, and students willing to learn more should definitely check out Kogan's course as well.
- More content on games, as much of the content is inherited from the [Computational Intelligence in Games](https://version.aalto.fi/gitlab/hamalap5/CIGCourse2018) course by Perttu Hämäläinen (Spring 2018, now replaced by this course)

[Machine Learning: Basic Principles](https://mycourses.aalto.fi/course/view.php?id=20569) at Aalto CS:

- We have some similar exercises, but the focus is more on applications and less on theory and mathematics, in order to make the material accessible for a wider audience. The more mathematically inclined students are highly encouraged to take both courses.

[Special Course in Computer Science D: Seminar on Computational Creativity](https://mycourses.aalto.fi/course/view.php?id=30459) at Aalto CS/ARTS:

- We focus on specific algorithms and their application, while this seminar focusses on theoretical questions regarding what makes a computational system creative, e.g. the very definition of creativity, means of evaluating creative systems, and the role of autonomy and intentionality. These questions are highly relevant to this course too, and we consequently provide a Computational Creativity crash course lecture on the last day of teaching.

[Elements of AI](https://www.elementsofai.com/):

- We have more practical exercises; this is a good follow-up if you've already taken that course.

## Syllabus

### Before the Course

Make sure you go through the materials and instructions on [this page](https://github.com/PerttuHamalainen/MediaAI/blob/master/Lessons/Prerequisites.md) before the course. This will take about 2 hours, plus an extra 1-2 hours if you install all the software on your computer. 

### Course Structure Overview

There will be **lectures on 7+ mornings from Tue-Fri**, and the remaining time is devoted to **project work** that can be done alone or together with a pair or group. The teachers will be there to provide personalized assistance. The exercises and project work are designed to scale for a broad range of skill levels. An overview below: 

|  | Monday | Tuesday | Wednesday | Thursday | Friday |
| --- | --- | --- | --- | --- | --- |
| Week 1 | - | Lecture: Overview | Lecture: Neural Network Basics | Lecture: Image Generation | Buffer |
| Week 2 | - | Lecture: Text Generation | Lecture: Optimization | Lecture: AI Playtesting | Lecture: Computational Creativity |
| Week 3 | - | Student Projects | Student Projects | Student Projects | Student Projects |

The lessons each include lecture slides and/or exercises. The exercises are in the form of Jupyter notebooks, and the links below open the notebooks in [Google Colab](https://colab.research.google.com/notebooks/intro.ipynb#), for which you need a Google account. [A note on Jupyter Notebooks, Colab, and other environments](Lessons/Jupyter_Colab_etc.md).

*Note that we don't expect all students to complete all the exercises.* If you can't figure something out, at least try reading and understanding the provided solutions. Also, please do ask for help.

Since most of the exercises are in Python, these [Python learning resources](Lessons/Python.md) might come handy. However, if you know some other programming language, you should be able to learn while going through the exercises.

### Detailed Course Structure

**DISCLAIMER**: The lecture slides are typically updated during each run of the course. Thus, if you download them before the course, you should re-download after the lecture has taken place.

**Week 1 (Tue 01.02 - Fri 04.02.)**

**Day 1 / Tue (Overview and Motivation — Perttu Hämäläinen):**

- Introduction: Each student adds a slide in a shared Google Slides document: What's your background, what do you want to learn? This will help students to find teammates and teachers to customize the course contents
- Lecture: [Overview and motivation](Lessons/LectureSlides/course_intro.pdf). Why one should rather co-create than compete with AI technology.
- Exercise: Introduction to tensors, numpy and matplotlib through processing images and audio. [[Open in Colab]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/ImagesAndAudio.ipynb), [[Solutions]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/ImagesAndAudio_solutions.ipynb)
- Related to the above, see also: [https://numpy.org/devdocs/user/absolute_beginners.html](https://numpy.org/devdocs/user/absolute_beginners.html)
- Exercise: Continuing the Numpy introduction, now for a simple data science project. [[Open in Colab]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/DataAndTensors.ipynb), [[Solutions]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/DataAndTensors_solutions.ipynb).
- Exercise: Training a very simple neural network using a [Kaggle](https://www.kaggle.com/) dataset of human height and weight. [[Open in Colab]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/PredictWeight.ipynb), [[Solutions]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/PredictWeight_solutions.ipynb)

**Day 2 / Wed (Neural Networks Basics — Perttu Hämäläinen):**

- Lecture: [Neural networks, part 1](Lessons/LectureSlides/neural_networks_part1.pdf). Basics of neural networks, focusing on discriminative models like image classification.
- Exercise: Image classification, the bread-and-butter of neural networks. [[Open in Colab]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/MNIST.ipynb), [[Solutions]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/MNIST_solutions.ipynb)
- Optional exercise: Fooling the image classifier with adversarial images. This is a bit more advanced topic, but included to demonstrate that discriminative models also have applications in generating images and visualizations. We will revisit the topic later. [[Open in Colab]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/AdversarialMNIST.ipynb).
- Optional exercise: Sound classification, to illustrate how processing audio can be done very similarly to images. Jupyter notebook: [https://github.com/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/AudioMNIST.ipynb](https://github.com/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/AudioMNIST.ipynb). Note: currently, this notebook only works on [Aalto's Jupyter server](Lessons/Jupyter_Colab_etc.md) or if you run it on your own computer after cloning the course Github repository. The audio file management has not yet been reworked for Colab.

**Day 3 / Thu (Image Generation Models — Nam Hee Gordon Kim):**

- Lecture: [Neural networks, part 2](Lessons/LectureSlides/neural_networks_part2.pdf). From discriminative to generative models like GANs. How to imbue computers with audiovisual imagination?
- Exercise: TBD. 

**Day 4 / Fri** **(Lecture Buffer / Exercises or Project Work):**

- (Optional) Additional lecture content to fit students’ skill levels
- Student exercises

**Week 2 (Tue 08.02 - Fri 11.02.)**

**Day 5 / Tue** **(Text Prediction and Generation Models — Perttu Hämäläinen):**

- Lecture: [Neural networks, part 3](Lessons/LectureSlides/neural_networks_part3.pdf). Sequential problems like text prediction and generation.
- Exercise: Create videogame text prompts on [OpenAI’s GPT-3 playground](https://beta.openai.com/overview)

**Day 6 / Wed (Optimization — Perttu Hämäläinen):**

- Lecture: [Optimization](Lessons/LectureSlides/optimization.pdf). Mathematical optimization is at the heart of almost all AI and ML. We've already applied optimization when training neural networks; now it's the time to get a bit wider and deeper understanding. We'll cover a number of common techniques such as Deep Reinforcement Learning (DRL), Monte Carlo Tree Search (MCTS), and Covariance Matrix Adaptation Evolution Strategy (CMA-ES).
- Exercise: [Generating abstract adversarial art using CMA-ES](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/CMA-ES_Art.ipynb)
- Unity example (a Unity project folder that you can download and open in Unity): [Discovering billiards trick shots in Unity](Code/Unity/IntelligentPool)

**Day 7 / Thu (AI Playtesting — Christian Guckelsberger)**

- Lecture: What is AI Playtesting? Use-cases for AI Playtesting based on industry requirements and research. State-of-the-art AI playtesting (Roohi et al., 2021): Combining deep reinforcement learning (DRL), Monte-Carlo tree search (MCTS) and a player population simulation to estimate player engagement and difficulty in a match-3 game. 
- Exercise: Validate goal reachability in a simple, procedurally generated game.

**Day 8 / Fri (Computational Creativity Theory — Christian Guckelsberger)**

- Lecture: Defining (computational) creativity from the perspectives of psychology vs. AI. Understanding generation and evaluation a two sides of the same coin. Discussing the need for autonomy and intentionality to increase the authenticity of creative systems.
- Exercise: Evaluate a system presented in the course based on a framework of your choice.

 

**Week 3 (Tue 15.02 - Fri 18.02.)**

Reserved for student projects. 

### Colab/Jupyter Notebooks and Other Tools

These can provide starting points for your project work. Note: the notebooks may take a while to run, but you can usually view or listen the pre-generated results without running the code. Disclaimer: I haven't had time to check that all of these run properly.

NOTE: if you get weird errors when running these on Colab, try adding `%tensorflow_version 1.x` to the beginning of the notebook, then select "Restart and run all" from the Runtime menu. Colab recently switched to Tensorflow 2 by default, but many of these notebooks precede the switch.

- [StyleGAN 2 finetuned with paintings](https://colab.research.google.com/drive/1cFKK0CBnev2BF8z9BOHxePk7E-f7TtUi#forceEdit=true&sandboxMode=true&scrollTo=jJRPjAU3Tn-K). Also see the [Reddit thread](https://www.reddit.com/r/MachineLearning/comments/bagnq6/p_stylegan_trained_on_paintings_512x512/?utm_content=title&utm_medium=post_embed&utm_name=44df278f7cb542cdb847cad34267b5ff&utm_source=embedly&utm_term=bagnq6)
- [Generate piano music with transformer networks](https://colab.research.google.com/notebooks/magenta/piano_transformer/piano_transformer.ipynb)
- [GPT-2 text generation and finetuning the model with custom text](https://colab.research.google.com/drive/1VLG8e7YSEwypxU-noRNhsv5dW4NfTGce)
- [NSynth Neural Audio Synthesis](https://colab.research.google.com/notebooks/magenta/nsynth/nsynth.ipynb). This is the "official" tutorial by Google.
- [A more extensive NSynth notebook](https://colab.research.google.com/drive/10wogut1V2ToGQZJcXH8StoMat6oWxoEu#scrollTo=0Fy4k_7SrnjH).
- [Neural style transfer for images](https://colab.research.google.com/github/tensorflow/lucid/blob/master/notebooks/differentiable-parameterizations/style_transfer_2d.ipynb)
- [Style transfer for textured 3D models](https://colab.research.google.com/github/tensorflow/lucid/blob/master/notebooks/differentiable-parameterizations/style_transfer_3d.ipynb)
- [BigGAN with an interactive interface](https://colab.research.google.com/github/tensorflow/hub/blob/master/examples/colab/biggan_generation_with_tf_hub.ipynb)
- [A big Github repository of Jupyter notebooks for various topics, e.g., text, voice, visualization](https://github.com/firmai/awesome-google-colab). To open the notebooks in Colab, replace the [https://github.com](https://github.com/) in the urls by [https://colab.research.google.com/github](https://colab.research.google.com/github).
- [12 Colab notebooks that matter](https://towardsdatascience.com/12-colab-notebooks-that-matter-e14ce1e3bdd0).
- [Google AI Hub notebooks](https://aihub.cloud.google.com/s?category=notebook&order=3)
- [Pix2Pix example](https://colab.research.google.com/github/tensorflow/docs/blob/master/site/en/tutorials/generative/pix2pix.ipynb#scrollTo=ITZuApL56Mny)
- A large repository of Jupyter notebook implementations of deep learning models (may or may not work on Colab): [https://github.com/rasbt/deeplearning-models](https://github.com/rasbt/deeplearning-models)
- [Deepfake tools](https://vuild.com/deep-fake-tools) (face swapping for images and video)
- [Machine learning plugins for GIMP](https://github.com/kritiksoman/GIMP-ML)

### Inspiration for Further Experiments

*a.k.a. Heroes of Creative AI and ML coding*

Here are some people who are mixing AI, machine learning, art, and design with awesome results:

- [http://otoro.net/ml/](http://otoro.net/ml/)
- [http://genekogan.com/](http://genekogan.com/)
- [http://quasimondo.com/](http://quasimondo.com/)
- [http://zach.li/](http://zach.li/)
- [http://memo.tv](http://memo.tv)
- [https://www.enist.org](https://www.enist.org/post/)

### Supplementary Material

- [ml5js](https://ml5js.org/) & [p5js](http://p5js.org/), the toolset that provides the fastest way to creative AI coding in a [browser-based editor](https://editor.p5js.org/), without installing anything. Works even on mobile browsers! [This example](https://editor.p5js.org/AndreasRef/sketches/r1_w73FhQ) uses a deep neural network to track your nose and draw on the webcam view. [This one](https://editor.p5js.org/genekogan/sketches/Hk2Q4Sqe4) utilizes similar PoseNet tracking to control procedural audio synthesis.
- [Machine Learning for Artists (ml4a)](http://ml4a.github.io/), including many cool [demos](http://ml4a.github.io/demos/), many of them built using p5js and ml5js.
- [Unity Machine Learning Agents](https://github.com/Unity-Technologies/ml-agents), a framework for using deep reinforcement learning for Unity. Includes code examples and blog posts.
- [Two Minute Papers](https://www.youtube.com/playlist?list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2), a YouTube channel with short and accessible explanations of AI and deep learning research papers.
- [3Blue1Brown](https://www.youtube.com/channel/UCYO_jab_esuFRV4b17AJtAw), a YouTube channel with excellent visual explanations on math, including [neural networks](https://www.youtube.com/playlist?list=PLZHQObOWTQDNU6R1_67000Dx_ZCJB-3pi) and [linear algebra](https://www.youtube.com/playlist?list=PLZHQObOWTQDPD3MizzM2xVFitgF8hE_ab).
- [Elements of AI](https://www.elementsofai.com/), an online course by University of Helsinki and Reaktor. Aalto students can also get 2 credits for this course. This is a course about the basic concepts, societal implications etc., no coding.
- [Game AI Book](http://gameaibook.org/) by Togelius and Yannakakis. PDF available.
- [Deep Learning book](https://www.deeplearningbook.org/) by Goodfellow et al. An excellent resource for digging deeper, for those that can handle some linear algebra, probability, and statistics. PDF available.

### Updates

*The field is changing rapidly and we are constantly collecting new teaching material.*

Follow the course's [Twitter feed](https://twitter.com/aaltomediaai) to stay updated. The twitter works as a public backlog of material that is used when updating the lecture slides.
