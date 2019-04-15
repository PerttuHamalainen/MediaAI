import os
from tensorflow.keras.utils import to_categorical
from pydub import AudioSegment
import numpy as np
import array
from helpers.playsound import update_soundpath
import ntpath

''' These are tools for loading arbitrary sets of wav files to use with machine learning. It turns wav files into numpy arrays that are good to input into models.
 Also, saves the numpy arrays on disk, so on the next run it can load the numpy arrays directly without needing to do a lot of conversion work.

Load audio loads audio from multiple folders and labels them according to the folder name. It does a 80-20 train-test split on the data.
Returns 4 arrays:
 X_train: Array of wav files in sample form as a numpy array. 80% of the data
 Y_train: Labels for X_Train, to train categorization.
 X_test: Array of wav files in sample form as a numpy array. 20% of the data
 Y_test: Labels for X_Test, to test categorization.

Attributes:
 foldername: Which folder under input to look into
 num_classes: How many folders within the main folder do we get examples from
 framerate: Changes the frame rate of the sound. Not very reliable currently.
 forceLoad: Do we ignore any previously saved version of the data and reload all of it.
 reshape: Do we reshape the data so that it fits better in a regular tensor.

 NOTE: The system also pads any sound to the longest length in the set. So try to get sounds of roughly the same length.
''' 



def load_audio(foldername, num_classes = 10, framerate = 0, forceLoad=False, reshape=True,inputpath = "../input/", amount_limit = 5000):
    folders_dir = inputpath + foldername
    name = folders_dir[(folders_dir.find("input/") + 6):]
    #print(name)
    if os.path.isfile(inputpath + "saved/" + name + ".npz") and not forceLoad and reshape:
        print("Library already loaded!")
        soundlibrary = np.load(inputpath + "saved/" + name + ".npz")
        x_train = (soundlibrary['arr_0'])
        y_train = (soundlibrary['arr_1'])
        x_test = (soundlibrary['arr_2'])
        y_test = (soundlibrary['arr_3'])
        path = (soundlibrary['arr_4'])
        update_soundpath("../" + str(path))
    else:
        x_train = []
        y_train = []
        x_test = []
        y_test = []
        category_folds = os.listdir(folders_dir)
        sound_duration = 0
        for i in range(1,num_classes + 1): #make this more stable, only find folders
            print("Loading soundset number {}".format(i))
            folder = folders_dir + "/" + category_folds[i-1] + "/"
            wav_fps = os.listdir(folder)
            print("{} sounds in category {}".format(len(wav_fps),category_folds[i-1]))
            clipamount = len(wav_fps)
            if(clipamount > amount_limit):
                clipamount = amount_limit
                print("limiting to "+  str(amount_limit)+" clips")
            trainamount = int(clipamount // 1.25)
            
            for j in range(0,trainamount):
                sound = AudioSegment.from_wav(folder + wav_fps[j])
                if framerate != 0:
                    #print("I WANT TO STAND OUT AND SHOW YOU THE FRAME RATE: " + str(sound.frame_rate))
                    sound = sound.set_frame_rate(framerate) # check frame rate and do this based on that. Silly to hard code.
                if(sound_duration == 0):
                    sound_duration = sound.duration_seconds
                sound = sound.set_channels(1) 
                soundarray = sound.get_array_of_samples()
                nparray = np.array(soundarray)
                x_train.append(nparray)
                y_train.append(i - 1)
            for j in range(trainamount,clipamount):
                sound = AudioSegment.from_wav(folder + wav_fps[j])
                if framerate != 0:
                    sound = sound.set_frame_rate(framerate) # check frame rate and do this based on that. Silly to hard code.
                sound = sound.set_channels(1) 
                soundarray = sound.get_array_of_samples()
                nparray = np.array(soundarray)
                x_test.append(nparray)
                y_test.append(i - 1)
        path = folder + wav_fps[0]
        update_soundpath(path)
        # Get longest clip from the data.
        max = 0
        for x in x_train:
            if len(x) > max:
                max = len(x)

        print("{} * {} is {}".format(framerate, sound_duration, framerate * sound_duration))
        if max <= framerate * sound_duration:
            if(framerate * sound_duration) % 2 != 0:
                max = int(framerate * sound_duration + 1)
            else:
                max = int(framerate * sound_duration)
        print("max is {}".format(max))

        # Pad data with zeroes so that all clips are the same length for convolution
        new_x_train = []
        for x in x_train:
            if len(x) < max:
                x = np.pad(x, (0, max-len(x)), mode='constant')
            new_x_train.append(x)
        x_train = np.array(new_x_train)

        new_x_test = []
        for x in x_test:
            if len(x) < max:
                x = np.pad(x, (0, max-len(x)), mode='constant')
            new_x_test.append(x)
        x_test = np.array(new_x_test)

        y_train = to_categorical(y_train) # this is a bit weird for tensorflow purposes, consider leaving this part out.
        y_test = to_categorical(y_test) # just give them without setting them one-hot.

        x_train, y_train = shuffleLists(x_train, y_train)
        #x_test, y_test = shuffleLists(x_train, y_train)
        x_test, y_test = shuffleLists(x_test, y_test)

        if(reshape):
            x_train = x_train.reshape(x_train.shape[0], x_train.shape[1], 1)
            x_test = x_test.reshape(x_test.shape[0], x_test.shape[1], 1)

        print('x_train shape:', x_train.shape)
        print('y_train shape:', y_train.shape)
        print('x_test shape:', x_test.shape)
        print('y_test shape:', y_test.shape)
        print(x_train.shape[0], 'train samples')
        print(x_test.shape[0], 'test samples')

        #name = folders_dir[(folders_dir.find("/") + 1):]
        name = ntpath.basename(folders_dir)
        print("Saving arrays to file")
        if not os.path.exists(inputpath + "saved/"):
            os.makedirs(inputpath + "saved/")
        np.savez(inputpath + "saved/" + name, x_train,y_train,x_test,y_test, path)
    return (x_train,y_train),(x_test,y_test)

'''Shuffles two lists so that they still map one-to-one.
 So that if the first value of list a is moved to third spot, 
 The first value of list b is also moved to the third spot.'''
def shuffleLists(a,b):
    indices = np.arange(a.shape[0])
    np.random.shuffle(indices)

    a = a[indices]
    b = b[indices]
    return a,b

'''Loads all sounds from one specific folder. Useful for GANs. Also saves them as a numpy array and writes it to disk for future runs.

Returns 1 array:
 X_train: Every sound in the folder as a numpy array file.

Attributes:
 foldername: Which folder under input to look into
 categoryname: Which folder from the main folder to load. (if empty just looks at main folder)
 framerate: Changes the frame rate of the sound. Not very reliable currently.
 forceLoad: Do we ignore any previously saved version of the data and reload all of it.
 reshape: Do we reshape the data so that it fits better in a regular tensor.
''' 
def load_all(foldername, categoryname ="",framerate = 0, forceLoad=False, reshape=True,inputpath = "../input/",save_array_to_file = False):
    folders_dir = inputpath + foldername + "/" + categoryname
    name = foldername + categoryname
    if os.path.isfile(inputpath + "saved/" + name + ".npz") and not forceLoad and reshape:
        print("Library already loaded!")
        soundlibrary = np.load(inputpath + "saved/" + name + ".npz")
        x_train = (soundlibrary['arr_0'])
        path = (soundlibrary['arr_1'])
        print(path)
        update_soundpath(path)
    else:
        x_train = []
        wavs = os.listdir(folders_dir)
        for wav in wavs:
            sound = AudioSegment.from_wav(folders_dir + "/" + wav)
            if framerate != 0:
                sound = sound.set_frame_rate(framerate) # check frame rate and do this based on that. Silly to hard code.
                #sprint("I WANT TO STAND OUT AND SHOW YOU THE FRAME RATE: " + str(sound.frame_rate*sound.duration_seconds))
            sound = sound.set_channels(1) 
            soundarray = sound.get_array_of_samples()
            nparray = np.array(soundarray)
            x_train.append(nparray)

        path = folders_dir + "/" + wavs[0]
        update_soundpath(path)
        # Get longest clip from the data.
        max = 0
        for x in x_train:
            if len(x) > max:
                max = len(x)

        # Pad data with zeroes so that all clips are the same length for convolution
        new_x_train = []
        for x in x_train:
            if len(x) < max:
                x = np.pad(x, (0, max-len(x)), mode='constant')
            new_x_train.append(x)
        x_train = np.array(new_x_train)

        if(reshape):
            x_train = x_train.reshape(x_train.shape[0], x_train.shape[1], 1)

        print('x_train shape:', x_train.shape)
        print(x_train.shape[0], 'train samples')

        # for x in x_train:
        #     check_sample(x)
        if save_array_to_file:
            print("Saving arrays to file")
            if not os.path.exists(inputpath + "saved/"):
                os.makedirs(inputpath + "saved/")
            np.savez(inputpath + "saved/" + name, x_train, path)
    return x_train


def segments2array(segments_list, framerate = 0, reshape=True):
    """
    convert a list of AudioSegment to a multi dimensional data array that can feed into neural network
    Returns 1 array:
     data_array: Every sound in the segments_list as a numpy array file.

    Attributes:
     segments_list: list of AudioSegments to be converted
     framerate: Changes the frame rate of the sound. Not very reliable currently.
     reshape: Do we reshape the data so that it fits better in a regular tensor.
    """
    data_array = []
    for sound in segments_list:
        if framerate != 0:
            sound = sound.set_frame_rate(framerate) # check frame rate and do this based on that. Silly to hard code.
            #sprint("I WANT TO STAND OUT AND SHOW YOU THE FRAME RATE: " + str(sound.frame_rate*sound.duration_seconds))
        sound = sound.set_channels(1) 
        soundarray = sound.get_array_of_samples()
        nparray = np.array(soundarray)
        data_array.append(nparray)

    # Get longest clip from the data.
    max = 0
    for x in data_array:
        if len(x) > max:
            max = len(x)

    # Pad data with zeroes so that all clips are the same length for convolution
    new_data_array = []
    for x in data_array:
        if len(x) < max:
            x = np.pad(x, (0, max-len(x)), mode='constant')
        new_data_array.append(x)
    data_array = np.array(new_data_array)

    if(reshape):
        data_array = data_array.reshape(data_array.shape[0], data_array.shape[1], 1)

    #print('data shape:', data_array.shape)
    #print(data_array.shape[0], 'data samples')

    return data_array    