# Njoud
A tiny game

# code best practises
(note that as this is written later in developement they might not always hold true. if you notice this try to fix it)

1. the variables a script uses should be public if accessed by other scripts and private if not(duh).
In addition private scripts should be [SerializField]s if their value is assigned in the editor, and public variables should be [HideInInspector] if they are assigned via code, to make clear if a variable is assigned via code or in the inspector.

2. scripts should start in UPPERCASE
3. public functions should start in UPPERCASE
4. rest is camelCased
5. everything with a healthbar is an instance of an entity 
