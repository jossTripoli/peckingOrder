# Pecking Order Game Development Process
 
This semester I embarked on an independent study exploring Unity game development and game AI. My main goal was to gain experience in game development and build skills and a portfolio that could help me pursue a career in the game industry. I found game development to be challenging yet rewarding, and greatly enjoyed the creativity involved in the programming, art, design, animation, and sound engineering of my own games.

I started my journey in game development by choosing to recreate classic games, such as Flappy Bird and PONG, as a way to learn the basics without being weighed down by having to be creative and make art and game design. I began by working on Flappy Tiel, a 2D game that helped me learn the Unity 2D game development flow including topics like C#, Visual Studio, sprites, animation, collisions, persisting high scores, the delta time approach, and creating game UI. Then through creating PONG I explored the concepts of multiplayer and creating enemy AI. As my knowledge and skills grew, I decided to challenge myself by making my own game.

## [Flappy Bird Demo](https://www.lehigh.edu/~jct324/FlappyTiel_WebGL/)
## [PONG Demo](https://www.lehigh.edu/~jct324/Pong_WebGL/)

I then delved deeper into game design and inspired by my recent favorite game called Polytopia, I researched 4X (Explore, Expand, Exploit, Exterminate) games. This led me to create a game design document for my own 4x game. Here is the game design document I created for my game Pecking Order:

## [Pecking Order Game Design Document](https://docs.google.com/document/d/1oWyOQN9Z7Rh0h8vXWrZLWEyG6qvI7BuZeyP3Ppp0API/edit?usp=sharing)

To learn how to create an isometric style game in Unity, I implemented a basic isometric grid via Unity Tilemap and manually placing all the tiles in place. I also learned about pathfinding by implementing A*. 

## [Manual Tilemap Demo](https://www.lehigh.edu/~jct324/Path_Finder_WebGL/)

After having to manually placing all the tiles in place for the isometric grid, I realized that this method was very tedious and would not allow for the game to be highly replayable. I wanted the user to be spawned in a new interesting map every time they played the game. So, I tried taking a randomly spawning tile game object into the map. However, this method didn't work well because the maps were too chaotic and not well-balanced between the different types of tiles. I also learned how to create pixel art using Aseprite. I explored new generative tools like DALL-E and hugging face to help speed up the process of coming up with assets for my game.

## [Random Map](https://www.lehigh.edu/~jct324/randomMapWebGL/)

I then tried using a bias random method to ensure more land than water was generated, but it still was too chaotic. That's when I discovered Perlin noise which is an algorithm that uses a pseudo-random sequence of values to generate a coherent pattern that mimics natural terrain features like hills and valleys. This method allowed me to create more diverse and interesting maps with a good balance of land, water, and mountain making the game much more enjoyable and replayable.

## [Perlin Noise Map](https://www.lehigh.edu/~jct324/perlinWorld/)

I continued by getting the cursor overlays to appear over water and mountain blocks changing colors to show the different elevations. I also worked on controls and camera functionality allowing it to pan, zoom, and spawn at the center of the tilemap. Another accomplishment was successfully implementing seeds for the world generation so I could continue to test with specific instances of maps created by the generator. 

Lastly, at the end of the semester I presented my work at the [Lehigh 2023 Expo](https://creativeinquiry.lehigh.edu/creative-inquiry/lehigh-expo) to prospective students. This was an incredible experience. I was able to share my knowledge and experience with others and it was truly rewarding to see the excitement and curiosity on their faces. The response from the visitors was overwhelmingly positive, and I received many compliments and encouraging feedback. I was particularly pleased to hear that many of the visitors were inspired by my work and were considering pursuing game development themselves. Moreover, at the Expo I was approached by a profess who was impressed with my skills and offered me a job opportunity. Although I already had summer plans and decided not to pursue this opprotunity, it was a testament to the value of the work I had done and the skills I had developed. 

Here's a demo of my game as of the end of Spring Semester 2023:

# [Final Demo](https://www.lehigh.edu/~jct324/PeckingOrderDemo/)

Overall, I'm incredibly proud of myself. I started the semester with zero experience in creating games and have made great progress in understanding game development and design, Unity, and game AI. I also gained experience in creating original pixel art. My next steps involve finishing up the exploration aspect of the game by adding a fog of war element to reveal blocks to the players and then starting the Expand, Exploit, Exterminate parts of the game!

## Detailed progress notes and my cited learning resources
[Slideshow](https://docs.google.com/presentation/d/1iPVDeZJxIL68Q6TMLT8nNq8rF_ULumYJtzw_htls-fw/edit?usp=sharing)
