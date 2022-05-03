Asset Creator - Vladislav Horobets (Hovl).
All that is in the folder "AAA Projectiles" can be used in commerce, even demo scene files.
-----------------------------------------------------

If you want to use post-effects like in the demo video:
https://youtu.be/hZSZ2Q8MF3k

Using:

1) Shaders
1.1)The "Use depth" on the material from the custom shaders is the Soft Particle Factor.
1.2)Use "Center glow"[MaterialToggle] only with particle system. This option is used to darken the main texture with a white texture (white is visible, black is invisible).
    If you turn on this feature, you need to use "Custom vertex stream" (Uv0.Custom.xy) in tab "Render". And don't forget to use "Custom data" parameters in your PS.
1.3)The distortion shader only works with standard rendering. Delete (if exist) distortion particles from effects if you use LWRP or HDRP!
1.4)You can change the cutoff in all shaders (except Add_CenterGlow and Blend_CenterGlow ) using (Uv0.Custom.xy) in particle system.

2)Light
2.1)You can disable light in the main effect component (delete light and disable light in PS). 
    Light strongly loads the game if you don't use light probes or something else.

3)Scripts
3.1)"AutoDestroyPS" is needed to destroy parts of the effects through a lifetime in the particle system.
    If you want projectiles to exist on the scene longer - just increase the Duration and Lifetime!
3.2)"ProjectileMover" is created for demonstration purposes only. The main product that you buy is effects.
    Use: Projectiles fly out with a “Fire Point” that you need to select on stage.
    The angle at which projectiles take off depends/isTheSame as on the object on which the script hangs.

4)How to modify the existing prefabs
4.1)If you reduce projectile speed, you also need to find the “Trail” tab in the particle system and increase the trail's lifetime.
    You also need to increase the Duration and Lifetime in all components with a particle system.
    When increasing speed, do the opposite.
4.2)When resizing projectiles, you need to change the value Emission> rate over distance if it exists in one of the components.
    If you double the size, you need to halve the "rate over distance" value.
    When reducing the size, do the opposite!
4.3)All Hits and Flashes can be resized using "transform" in the main component.
4.4)Tutorial how to make target projectile: https://www.youtube.com/watch?v=LJLWNnqAjQ4

5)Quality
5.1) For better sparks quality enable "Anisotropic textures: Forced On" in quality settings.

6)SUPPORT ASSET FOR URP(LWRP) or HDRP here --> https://assetstore.unity.com/packages/slug/157764
  SUPPORT ASSET FOR URP(LWRP) or HDRP here --> https://assetstore.unity.com/packages/slug/157764
  SUPPORT ASSET FOR URP(LWRP) or HDRP here --> https://assetstore.unity.com/packages/slug/157764

Contact me if you have any problems or questions.
My email: gorobecn2@gmail.com