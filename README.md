# AI Research Project
## Purpose:
1. To study and utilize Unity's ML agents.
2. To create my own AI script that thinks for itself using a JSON file of cardinal directions. 
3. Utilization of SO to contain the cardinal directions such that I can see the directions in the inspector.

## Things to keep in mind:

### Unity's ML Agents
- Unity's ML agent package does not give much context as to exactly how to use it. Due to the lack of documentation, I've decided to create my own AI that thinks and moves accordingly then saves it's movement accordingly. 
My information on ML agents is as follows:
1. The use of a neuro network file is needed (and is unreadable to humans). This file determines what the AI is going to do. An AI is supposed to act upon what it's been trained to do, so you have to train the AI manually and record the training session towards your goal. The scripting must be done towards saving the prior actions and awards to the neuro network file.

### My Custom agent
- This custom agent's goal and purpose is to think then move. It has a token system kinda like ML Agents, but the core functionality is to ensure the AI saves and loads the cardinal directions that would reward them the most.
- Once the cardinal direction is saved and the goal has been reached, the AI loads from it (using it's memory).

### How? Serialization.
- Serialization is the process of converting the state of an object into a form that can be persisted or transported. Imagine placing things into a box and putting tape around it.
- Deserialization essentially does the opposite.
#### Why is this important?
- Because serialization allows us to save to a JSON file.
Example: `string text = JsonUtility.ToJson((serialized data we wish to save), (pretty print)); `
then: `string fileName = Path.Combine(Application.persistentDataPath, "Example.json").Replace('\\', '/'); //This also creates a JSON if it doesn't exist and names it Example.JSON then it replaces the \\ with / for sanity sake`
finally: `File.WriteAllText(fileName, text); // gives the final where and what`
- Deseralization allows us to take old information from the JSON file.
Example: we **MUST** check to see if it exists first and foremost
`if (!File.Exists(fileName))
            return;`
then: `string text = File.ReadAllText(fileName);`
Optional: If you'd like to overwrite information, call the FromJsonOverwrite (in my case) `JsonUtility.FromJsonOverwrite(text, thoughtProcess);// thought process gets overwritten with text from above.`