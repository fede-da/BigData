# Overview

This document provides a general overview about the project, its structure, requirements and how to use it.

## Structure

Each section contains different pieces of information about a specific matter, here's a rapresentation of project's structure:

```
├── data
│   ├── processed
│   └── raw 
├── models
│   ├── pretrained
│   └── finetuned
├── notebooks
│   ├── 
│   └── 
├── scripts
│   ├── data_preprocessing.py
│   └── trainings/
└── services
    ├── ai_service.py
    └── utils
```

Here's a break down:

* **[Data](./data/README.md)**: Contains all the _raw_ and _processed data_ required to train the model.


* **[Models](./models/README.md)**: Houses the _pretrained_ and _finetuned_ machine learning models.


* **[Notebooks](./notebooks/README.md)**: Jupyter notebooks used for _exploratory data analysis_, _model evaluation_, and any other R&D purposes. A great place to visualize data and model performance.


* **[Scripts](./scripts/README.md)**: Python scripts dedicated to tasks like _data preprocessing_, _model training_...


* **[Services](./services/README.md)**: Includes some tools to interface with the project.



##  Requirements

This section contains all the prerequisites and packages needed to run the project. 

Before proceeding please double-check if your GPU supports _CUDA Development Tools_ from official [documentation](https://docs.nvidia.com/cuda/cuda-installation-guide-microsoft-windows/index.html#:~:text=You%20can%20verify%20that%20you,that%20GPU%20is%20CUDA%2Dcapable.).

Please note that training without available GPU uses CPU instead so it could be quite slow, this is true for both solutions. Visit [script](./scripts/README.md) section to learn more.

This project's packet manager is _pip_, if you don't have it read official [documentation](https://pip.pypa.io/en/stable/installation/). By the way, _conda_ works fine too.

##  PyTorch

`PyTorch` is an optimized tensor library for deep learning using `GPUs` and `CPUs` developed by Facebook's AI Research lab especially useful when it comes to neural networks; it supports `CUDA` to heavy computing speed up. 

Further information available [here](https://pytorch.org/docs/stable/index.html)

#### Packages

The following packages are required to run the program:

- `transformers`: Set of pretrained models to perform tasks, to read more follow the [link](https://pypi.org/project/transformers/)
- `torch`

Open your terminal and run the followings:

```
pip install torch

pip install transformers
```

Then install Nvidia [toolkit](https://developer.nvidia.com/cuda-downloads?target_os=Windows&target_arch=x86_64&target_version=Server2022&target_type=exe_local) and run:

```
pip install torch torchvision torchaudio --index-url https://download.pytorch.org/whl/cu117
```

**N.B.:** Please check the output and edit `PATH` environment variable if needed. To complete the installation on `Windows` enabling `Win32 Long Paths` is required, so take a look at the following [link](https://www.thewindowsclub.com/how-to-enable-or-disable-win32-long-paths-in-windows-11-10?utm_content=cmp-true) otherwise follow the instructions below:

To enable Win32 long paths through Regedit:

1. Open Regedit
2. Paste the path for the file system folder
3. Find the LongPathsEnabled DWORD file and double-click on it
4. Change to value from 0 to 1 and click OK

To achieve above instructions navigate to 

`HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\FileSystem` 

then double-click on `LongPathEnabled` and set its value to `1`.






















