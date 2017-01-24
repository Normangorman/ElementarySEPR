# ElementarySEPR
Git repo for our team!

# Contributing
Get git: https://git-for-windows.github.io/

Configure Unity to work with Git:

![Unity git config](https://dl.dropboxusercontent.com/u/80523064/unity_git_config1.PNG)
![Unity git config](https://dl.dropboxusercontent.com/u/80523064/unity_git_config2.PNG)

We are following the Git strategy described in this article: http://nvie.com/posts/a-successful-git-branching-model/
![Git branching strategy](http://nvie.com/img/git-model@2x.png)

When you want to add a new feature, checkout the latest commit on the develop branch and make a new branch for your feature.

Example of doing this:

    git checkout develop                    # this switches you to the latest commit on the develop branch
    git checkout -b myname/my-cool-feature  # this creates a new branch for your feature
    # ... do some stuff
    git add file1 file2 file3               # add the files you changed
    git commit -m "I did some stuff"        # make a commit with a sensible message
    git push origin myname/my-cool-feature  # push your branch to GitHub
    # now go to GitHub and open a merge request for 'myname/my-cool-feature' to 'develop'
    
Both the develop branch and the master branch are protected and require any group member to review the merge request before it is accepted.
