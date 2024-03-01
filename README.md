# AI for Media, Art & Design

<!--
![Some examples of content covered](Lessons/Media/github_preview.jpg)
-->
![Images created with the CLIP (Contrastive Language-Image Pre-training; Radford et al., 2021) model](Lessons/Media/clip_256_wide.jpg)

This repository contains the lectures and materials of Aalto University's AI for Media, Art & Design course. Scroll down for lecture slides and exercises.

Follow the course's [Twitter feed](https://twitter.com/aaltomediaai) for links and resources.

## Course Overview & Design Philosophy

This is a hands-on, project-based crash course for deep learning and other AI techniques for people with **as little technical prerequisites as possible.** The focus is on **media processing and games**, which makes this particularly suitable for **artists and designers**.

The 2024 edition of this course is taught during Aalto University's Period 3 (six weeks) by **Prof Perttu Hämäläinen**  ([Twitter](https://twitter.com/perttu_h)) and **Nam Hee Gordon Kim.** ([Twitter](https://twitter.com/NamHeeGordonKim)). Registeration through [Aalto's Mycourses system](https://mycourses.aalto.fi), specific inquiries to: perttu.hamalainen[at]aalto.fi (but check the info below first).

### Learning Goals

The goal is for students to:

- understand how common AI algorithms & tools work,
- understand what the tools can be used for in context of art, media, and design, and
- get hands-on practice of designing, implementing and/or using the tools.

### Materials and pedagogical approach

The course is taught through:

- Lectures
- Exercises that require you to either practice using existing AI tools, programmatically utilize the tools (e.g., to automate tedious manual prompting), or build new systems. We always try to provide both easy and advanced exercises to cater for different skill levels.
- Final project on topics based on each student's interests. This can also be done in pairs.  

The exercises and project work are designed to scale for a broad range of skill levels and starting points.

Outside lectures and exercises, we use **Teams** for sharing results and peer-to-peer tutoring and guidance (Teams invitation will be send to registered students).

### Student Prerequisites

Although many of the exercises do require some Python programming and math skills, one can complete the course without programming, by focusing on creative utilization of existing tools such as ChatGPT and DALL-E.

### Grading / Project Work

You **pass the course** by submitting a report of your project in [MyCourses](http://mycourses.aalto.fi). The grading is pass / fail, as numerical grading is not feasible on a course where students typically come from very different backgrounds. To get your project accepted, the main requirement is that a you make an effort and advance from your individual starting point.

It is also recommended to make the project publicly available, e.g., as a Colab notebook or Github repository. Instead of a project report, you may simply submit a link to the notebook or repository, if they contain the needed documentation.

Students can choose their **project topics** based on their own interests and learning goals. Projects are agreed on with the teachers. You could create something in Colab or Unity Machine Learning agents, or if you'd rather not write any code, experimenting with artist-friendly tools like [GIMP-ML](https://github.com/kritiksoman/GIMP-ML) or [RunwayML](https://runwayml.com/) to create something new and/or interesting is also ok. For example, one could generate song lyrics using a text generator such as GPT-3, and use them to compose and record a song.

## Preparing the Course

**Before the first lecture, you should:**

* Prepare to add one slide to a shared Google Slides document (link provided at the first lecture), including 1) your name and photo, 2) your background and skillset, 3) and what kind of projects you want to work on. This will be useful for finding other students with similar interests and/or complementary skills. If you do not yet know what to work on, browse the [Course Twitter](https://twitter.com/aaltomediaai) for inspiration.

* Make yourself a Google account if you don't have one. This is needed for [Google Colab](https://colab.research.google.com/notebooks/intro.ipynb#), which we use for many demos and programming exercises. **Important security notice:** Colab notebooks run on Google servers and by default cannot access files on your computer. However, some notebooks might contain malicious code. Thus, *do not input any passwords or let a Colab notebook connect with your Google Drive* unless you trust the notebook. The notebooks in this repository should be safe but the lecture slides also link to 3rd party notebooks that one can never be sure about.

* For using OpenAI tools, it's also good to get an [OpenAI account](https://platform.openai.com). When creating the account, you get some free quota for generating text and images.

* If you plan to try the programming exercises of the course, these [Python learning resources](Lessons/Python.md) might come handy. However, if you know some other programming language, you should be able to learn while going through the exercises.

* To grasp the fundamentals of what neural networks are doing, watch episodes 1-3 of 3Blue1Brown's [neural network series](https://www.youtube.com/playlist?list=PLZHQObOWTQDNU6R1_67000Dx_ZCJB-3pi)

* Learn about Colab notebooks (used for course exercises) by watching these videos: [Video 1](https://www.youtube.com/watch?v=inN8seMm7UI), [Video 2](https://www.youtube.com/watch?v=PitcORQSjNM) and reading through this [Tutorial notebook](https://colab.research.google.com/notebooks/welcome.ipynb). Feel free to also take a peek at this course's exercise notebooks such as [GAN image generation](https://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/BigGAN%20test.ipynb). To test the notebook, **select "run all"** from the **"runtime" menu**. This runs all the code cells in sequence and should generate and display some images at the bottom of the notebook. In general, when opening a notebook, it's good to use "run all" first, before starting to modify individual code cells, because the cells often depend on variables initialized or packages imported in the preceding cells.


## Syllabus and contents
The following provides the lecture slides and exercises. We will progress roughly one day per topic, devoting the last sessions to individual project work.

**Overview and Motivation:**

- Lecture: [Overview and motivation](Lessons/LectureSlides/course_intro.pdf). Why one should rather co-create than compete with AI technology.
- Exercise: Each student adds their slide to the shared Google Slides. We will then go through the slides and briefly discuss the topics and who might benefit from collaborating with others.

Optional programming exercises for those with at least some programming background:
- Exercise: If you didn't already do it, go through the Colab learning links in the [course prerequisites](https://github.com/PerttuHamalainen/MediaAI/blob/master/Lessons/Prerequisites.md).
- Exercise: Introduction to tensors, numpy and matplotlib through processing images and audio. [[Open in Colab]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/ImagesAndAudio.ipynb), [[Solutions]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/ImagesAndAudio_solutions.ipynb)
- Related to the above, see also: [https://numpy.org/devdocs/user/absolute_beginners.html](https://numpy.org/devdocs/user/absolute_beginners.html)
- Exercise: Continuing the Numpy introduction, now for a simple data science project. [[Open in Colab]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/DataAndTensors.ipynb), [[Solutions]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/DataAndTensors_solutions.ipynb).
- Exercise: Training a very simple neural network using a [Kaggle](https://www.kaggle.com/) dataset of human height and weight. [[Open in Colab]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/PredictWeight.ipynb), [[Solutions]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/PredictWeight_solutions.ipynb)



**Text Generation & Co-writing with AI:**

- Lecture: [Co-writing with AI](Lessons/LectureSlides/Writing_with_AI.pdf). Introduction to Large Language Models (LLMs), some history, examples of different types of texts and applications.

- Lecture: [Deeper into transformers](Lessons/LectureSlides/deeper_into_transformers.pdf). Understanding transformer attention and emergent abilities of LLMs.

- Exercises: [Generating game ideas, automating manual prompting using Python, Retrieval-Augmented Generation](Lessons/LectureSlides/Writing_with_AI_exercises.pdf)

- Tutorial: [Few-shot Prompting using OpenAI API](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/few_shot_prompting.ipynb)

**Image Generation**
- Lecture: [Image generation](Lessons/LectureSlides/image_generation.pdf)

- Setup: If you have 2060 GPU or better, **consider installing Stable Diffusion locally** - no subscription fees, no banned topics or keywords. It's best to install Stable Diffusion with a UI such as [ComfyUI](https://github.com/comfyanonymous/ComfyUI) or  [Foocus](https://github.com/lllyasviel/Fooocus), which provide advanced features like [ControlNet](https://comfyanonymous.github.io/ComfyUI_examples/controlnet/) that allows you to input a rough image sketch to control the layout of the generated images.

- Prompting Exercise: Prompt images with different art styles, cameras, lighting... For reference, see [The DALL-E 2 prompt book](https://dallery.gallery/wp-content/uploads/2022/07/The-DALL%c2%b7E-2-prompt-book.pdf) Use your preferred text-to-image tool such as DALL-E, MidJourney or Stable Diffusion. You can install Stable Diffusion locally (see above), and it is also available through Colab: [Huggingface basic notebook](https://colab.research.google.com/github/huggingface/notebooks/blob/main/diffusers/stable_diffusion.ipynb), [Notebook with Automatic1111 WebUI](https://colab.research.google.com/github/TheLastBen/fast-stable-diffusion/blob/main/fast_stable_diffusion_AUTOMATIC1111.ipynb), [Notebook with Foocus UI](https://colab.research.google.com/github/lllyasviel/Fooocus/blob/main/fooocus_colab.ipynb). If you prefer a mobile app, some free options are Microsoft Copilot for ChatGPT and DALL-E on [iOS](https://apps.apple.com/us/app/microsoft-copilot/id6472538445) or [Android](https://apps.apple.com/us/app/microsoft-copilot/id6472538445), or [Draw Things for Stable Diffusion](https://drawthings.ai/)

- Prompting Exercise: Pick an interesting reference image and try to come up with a text prompt that produces an image as close to the reference as possible. This helps you hone your descriptive English writing skills.

- Prompting Exercise: Practice using both text and image prompts using StableDiffusion and ControlNet. This can be done either in [Colab](https://colab.research.google.com/github/huggingface/notebooks/blob/main/diffusers/controlnet.ipynb#scrollTo=4_lkdXOQmjnV), installing Stable Diffusion locally (see above), or using a mobile app such as [Draw Things](https://drawthings.ai/)

- Prompting Exercise: Make your own version of [making a bunny happier](https://www.reddit.com/r/ChatGPT/comments/1839fgo/asking_gpt_to_make_a_bunny_happier/), progressively exaggerating some other aspect of some initial prompt.

- Colab Exercise: Using a Pre-Trained Generative Adversarial Network (GAN) to generate and interpolate images. [[Open in Colab]](https://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/BigGAN%20test.ipynb), [[Solutions]](https://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/BigGAN%20test_solutions.ipynb)

- Colab Exercise: [Interpolate between prompts using Stable Diffusion](https://colab.research.google.com/github/keras-team/keras-io/blob/master/examples/generative/ipynb/random_walks_with_stable_diffusion.ipynb)

- Colab Exercise: Finetune StableDiffusion using your own images. There are multiple options, although all of them seem to require at least 24GB of GPU memory => you'll most likely need a paid Colab account. Some options: [Huggingface Diffusers official tutorial](https://colab.research.google.com/github/huggingface/notebooks/blob/main/diffusers/sd_dreambooth_training.ipynb), [Joe Penna's DreamBooth](https://github.com/JoePenna/Dreambooth-Stable-Diffusion), [TheLastBen](https://github.com/TheLastBen/fast-stable-diffusion)


**Generating other media, real-life workflows**
- Lecture: [Generating other media](Lessons/LectureSlides/other_generation.pdf)


**Optimization**

- Lecture: [Optimization](Lessons/LectureSlides/optimization.pdf). Mathematical optimization is at the heart of almost all AI and ML. We've already applied optimization when training neural networks; now it's the time to get a bit wider and deeper understanding. We'll cover a number of common techniques such as Deep Reinforcement Learning (DRL) and Covariance Matrix Adaptation Evolution Strategy (CMA-ES).
- Exercise: Experiment with abstract art generation using [CLIPDraw](https://colab.research.google.com/github/kvfrans/clipdraw/blob/main/clipdraw.ipynb) and [StyleCLIPDraw](https://colab.research.google.com/github/pschaldenbrand/StyleCLIPDraw/blob/master/Style_ClipDraw.ipynb). First, follow the notebook instructions to get the code to generate something. Then try different text prompts and different drawing parameters.
- Exercise (hard, optional): Modify CLIPDraw or StyleCLIPDraw to use CMA-ES instead of Adam. This should allow more robust results if you use high abstraction (only a few drawing primitives), which tends to make Adam more probable to get stuck in a bad local optimum. For reference, you can see this old course exercise on [Generating abstract adversarial art using CMA-ES](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/CMA-ES_Art.ipynb). Note: You can also combine CMA-ES and Adam by first finding an approximate solution with CMA-ES and then finetuning with Adam.
- Unity exercise (optional): [Discovering billiards trick shots in Unity](Code/Unity/IntelligentPool). Download the project folder and test it in Unity.

**Game AI**

- Lecture: [Game AI](Lessons/LectureSlides/game_AI.pdf) What is game AI? Game AI Research in industry / academia. Core areas of videogame AI. Deep Dive: State-of-the-art AI playtesting (Roohi et al., 2021): Combining deep reinforcement learning (DRL), Monte-Carlo tree search (MCTS) and a player population simulation to estimate player engagement and difficulty in a match-3 game.
- Exercise: Deep Reinforcement Learning for General Game-Playing [[Open in Colab]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/GameAI.ipynb). [[Open in Colab with Solutions]](http://colab.research.google.com/github/PerttuHamalainen/MediaAI/blob/master/Code/Jupyter/GameAI_Solutions.ipynb).

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
The lecture slides have more extensive links to resources on each covered topic. Here, we only list some general resources:

- [ml5js](https://ml5js.org/) & [p5js](http://p5js.org/), if you prefer Javascript to Python, this toolset may provides the fastest way to creative AI coding in a [browser-based editor](https://editor.p5js.org/), without installing anything. Works even on mobile browsers! [This example](https://editor.p5js.org/AndreasRef/sketches/r1_w73FhQ) uses a deep neural network to track your nose and draw on the webcam view. [This one](https://editor.p5js.org/genekogan/sketches/Hk2Q4Sqe4) utilizes similar PoseNet tracking to control procedural audio synthesis.
- [Machine Learning for Artists (ml4a)](http://ml4a.github.io/), including many cool [demos](http://ml4a.github.io/demos/), many of them built using p5js and ml5js.
- [Unity Machine Learning Agents](https://github.com/Unity-Technologies/ml-agents), a framework for using deep reinforcement learning for Unity. Includes code examples and blog posts.
- [Two Minute Papers](https://www.youtube.com/playlist?list=PLujxSBD-JXglGL3ERdDOhthD3jTlfudC2), a YouTube channel with short and accessible explanations of AI and deep learning research papers.
- [3Blue1Brown](https://www.youtube.com/channel/UCYO_jab_esuFRV4b17AJtAw), a YouTube channel with excellent visual explanations on math, including [neural networks](https://www.youtube.com/playlist?list=PLZHQObOWTQDNU6R1_67000Dx_ZCJB-3pi) and [linear algebra](https://www.youtube.com/playlist?list=PLZHQObOWTQDPD3MizzM2xVFitgF8hE_ab).
- [Elements of AI](https://www.elementsofai.com/), an online course by University of Helsinki and Reaktor. Aalto students can also get 2 credits for this course. This is a course about the basic concepts, societal implications etc., no coding.
- [Game AI Book](http://gameaibook.org/) by Togelius and Yannakakis. PDF available.
- [Understanding Deep Learning](https://udlbook.github.io/udlbook/) by Simon J.D. Prince. Published in December 2023, this is currently the most up-to-date textbook for deep learning, praised for its clear explanations and helpful visualizations. An excellent resource for digging deeper, for those that can handle some linear algebra, probability, and statistics. PDF available.

### Updates

*The field is changing rapidly and we are constantly collecting new teaching material.*

Follow the course's [Twitter feed](https://twitter.com/aaltomediaai) to stay updated. The twitter works as a public backlog of material that is used when updating the lecture slides.
