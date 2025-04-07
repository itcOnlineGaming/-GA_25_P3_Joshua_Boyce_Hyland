# Enemy System Guide

Here you will see how to use the Enemy system package

---

## Table of Contents
1. [Set-Up](#set-up)
2. [Enviorment](#enviorement)
3. [Enemy](#enemy)


---

## Set Up
You need to add this component to your Packages/manifest file

    "ie.setu.enemy": "https://github.com/itcOnlineGaming/-GA_25_P3_Joshua_Boyce_Hyland.git?path=/Enemy/Packages/ie.setu.enemy"

---

## Enviorment

### Overview

Here you can choose to create the enviorment the character will be walking on, with a choice of a sphere or some sort of plane : .

#### Use

##### Sphere enviorement set up

1. Add the `Atmosphere` script to your sphere world.
2. Adjust the radius to be bigger than the world. As an acutally atmosphere would. <br>
<img src="gifs/atmosphere.gif" width="600" alt="Demo GIF"/>



## Enemy

### Overview:

Here you can see how to create an enemy.

1. Attack an enemy script to an empty game object <br>
<img src="gif/1.gif" width="600" alt="Demo GIF"/>
2. Assign the planet of the enemy scriipt <br>
<img src="gif/2.gif" width="600" alt="Demo GIF"/>
3. Give the Gameobject an animation Manager <br>
<img src="gif/3.gif" width="600" alt="Demo GIF"/>
4. Assign this to the enemy Manager<br>
<img src="gif/4.gif" width="600" alt="Demo GIF"/>
5. Create a animation script which will implement an attack and a death function <br>
<img src="gif/5.gif" width="600" alt="Demo GIF"/>
6. Assign these functions as animation events for your chosen animation.<br>
<img src="gif/6.gif" width="600" alt="Demo GIF"/>
7. Assigne these animattion in the default animation controller<br>
<img src="gif/7.gif" width="600" alt="Demo GIF"/>
8. Assign this controller to the animation manager.<br>
<img src="gif/8.gif" width="600" alt="Demo GIF"/>

<br>
<img src="gif/9.gif" width="600" alt="Demo GIF"/>


