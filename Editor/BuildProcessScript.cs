using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class BuildProcessScript 
{
	[PostProcessBuild]
	public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) 
	{
		Debug.Log("PTBP " + pathToBuiltProject );
		Debug.Log("Targ" + target.ToString());
		//copy the folders across now
		//Pity there isn't a 'copy if newer' type affair
		//Still, builds are relatively rare
		
		//pathToBuiltProject is mroe like pathToBuiltEXE, ffs
		//fuck you. Twats
		int c;
		for(c = pathToBuiltProject.Length-1;c>0;c--)
		{
			if (pathToBuiltProject[c] == '/') break;//fucking yay
			
		}
		pathToBuiltProject = pathToBuiltProject.Remove(c);//wankers
		Debug.Log("Stripped [" + pathToBuiltProject +"]");//fucking... fuck off. Stupid.
		
		DirectoryCopy("Assets/Default",pathToBuiltProject+"/Default",true);
		
	
	 
	}
	// ***********************************************************************************************
	private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
    {
		Debug.Log("Copying from [" + sourceDirName +"] to [" + destDirName +"]");
        // Get the subdirectories for the specified directory.
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        DirectoryInfo[] dirs = dir.GetDirectories();

        if (!dir.Exists)
        {
            throw new DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        // If the destination directory doesn't exist, create it. 
        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        // Get the files in the directory and copy them to the new location.
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, true);
        }

        // If copying subdirectories, copy them and their contents to new location. 
        if (copySubDirs)
        {
            foreach (DirectoryInfo subdir in dirs)
            {
				if(subdir.Name.StartsWith(".svn"))
					continue;
				
                string temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, copySubDirs);
            }
        }
    }
	// ***********************************************************************************************
	
}
