''' Tools for playing and saving sounds. They can play straight from the terminal, or be stored as a waveform graph and wav file.''' 

from pydub import AudioSegment
from pydub.playback import play
import matplotlib.pyplot as plt
import array
import time
import os
import numpy as np
import pandas as pd

plt.rcParams['agg.path.chunksize'] = 10000
soundpath = ''

''' This is a helper function that makes sure that sounds are played at the corret framerate. 
Attributes:
 path: Path to a representable sound from the category'''
def update_soundpath(path):
    global soundpath
    soundpath = str(path)
    #print("soundpath updated to " + soundpath)

'''Simple function that just plays any array of samples it is given straight in the terminal.
Atttributes
 sample: Array of samples to play
 label: A name for what type the sound being played is.
 upscale: Do we scale the sound from a scale of 0 to 1 into a scale of -32768 to 32768. 
'''
def play_sound(sample, label, upscale=False): # We don't know what the original file was like at this point anymore. AKA length and framerate. This works for now
    #sound = AudioSegment.from_file('input/speech_commands/bed/1bb574f9_nohash_0.wav')
    sound = AudioSegment.from_file(soundpath)
    playsound = sample[0]
    if upscale:
        playsound = upscale_sample(playsound)
    shifted_samples_array = array.array(sound.array_type, playsound)
    new_sound = sound._spawn(shifted_samples_array)
    print("playing sound from category " + str(label))
    play(new_sound)

'''Plays and saves a sound to disk, both as a plot and a sound file. 
 sample: Array of samples to play
 label: A name for what type the sound being played is.
 upscale: Do we scale the sound from a scale of 0 to 1 into a scale of -32768 to 32768. 
'''
def play_and_save_sound(samples, folder, run_name="", epoch=0, upscale=True):
    #check_sample(samples[0])
    #sound = AudioSegment.from_file('input/speech_commands/bed/1bb574f9_nohash_0.wav')
    sound = AudioSegment.from_file(soundpath)
    sound.set_channels(1)
    print(sound.array_type)
    playsound = samples[0]
    if upscale:
        playsound = upscale_sample(playsound)
    sample_array = array.array(sound.array_type, playsound)
    new_sound = sound._spawn(sample_array)
    if not os.path.exists("output/" + folder + "/"):
        os.makedirs("output/" + folder + "/")
    #print(sample_array)
    plt.figure(figsize=(30,10))
    plt.ylim(-32768, 32768)
    plt.plot(sample_array)
    plt.savefig("output/" + folder + "/" + run_name + "#" + str(epoch))
    plt.clf()
    plt.cla()
    print("playing and saving sound from category " + str(run_name) + " folder " + folder)
    play(new_sound)
    new_sound.export("output/" + folder + "/" + run_name + "#" + str(epoch) + ".wav", format="wav")

# Only saves the sound to disk, plotted and as a sound file. 
def save_sound(samples, folder, run_name="", epoch=0, upscale=True, index=0, notebook=False):
    #sound = AudioSegment.from_file('input/speech_commands/bed/1bb574f9_nohash_0.wav')
    global soundpath
    sound = AudioSegment.from_file(soundpath)
    sound.set_channels(1)
    #check_sample(samples[index])
    playsound = samples[index]
    if upscale:
        playsound = upscale_sample(playsound)
    playsound = np.clip(playsound, -32768,32767)
    #check_sample(playsound)
    #print(playsound.shape)
    sample_array = array.array(sound.array_type, playsound)
    new_sound = sound._spawn(sample_array)
    if not os.path.exists("output/" + folder + "/"):
        os.makedirs("output/" + folder + "/")
    #print(sample_array)
    filepath = "output/" + folder + "/" + run_name + "#" + str(epoch)
    if(notebook):
        notebook_plot_sound(sample_array,filepath)
    else:
        plot_sound(sample_array,filepath)
    print("saving sound from category " + str(run_name) + " folder " + folder)
    new_sound.export(filepath + ".wav", format="wav")
    return filepath + ".wav"

# A version of the function that works speficially in Jupyter Notebooks.
def notebook_plot_sound(sample_array, filepath):
    plt.figure(figsize=(30,10))
    max = check_scale(sample_array)
    plt.ylabel('Amplitude')
    plt.xlabel('Time')
    plt.ylim(-max, max)
    plt.plot(sample_array)
    plt.show()

# Plotting function to run in the terminal to save a plot to disk.
def plot_sound(sample_array, filepath):
    plt.figure(figsize=(30,10))
    plt.ylabel('Amplitude')
    plt.xlabel('Time')
    plt.ylim(-32768, 32768)
    plt.plot(sample_array)
    plt.savefig(filepath)
    plt.clf()
    plt.cla()
    
# Gives a numerical overview of the sample array. Good for debugging generators.
def check_sample (sample):
    s = pd.Series(sample.flatten().tolist())
    print(s.describe())

#obsolete function to circumvent some generation problem.
def check_scale (sample):
    mini = 10000
    maxi = 0
    for i in sample:
        if(i < mini):
            mini = i
        if(i > maxi):
            maxi = i
    return max(abs(maxi), abs(mini))
    
# Upscales a sample from a 0-1 normalized presentation to something listenable. 
def upscale_sample(sample): 
    #check_sample(sample)
    new_sample = sample * 65536
    new_sample = new_sample - 32768
    #check_sample(new_sample)
    return new_sample.astype(int)