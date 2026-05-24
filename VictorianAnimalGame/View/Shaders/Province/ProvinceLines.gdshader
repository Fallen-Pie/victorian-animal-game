shader_type canvas_item;

uniform vec4 line_color : source_color = vec4(0.0, 1.0, 0.0, 1.0);
uniform float dot_frequency = 0.05; // How often dots appear (0.0 - 1.0)
uniform float animation_speed = 5.0; // How fast they "march"

void fragment() {
	// 1. Get the progress along the length of the line. 
	// In Godot 2D drawing, UV.x usually gives us progress from 0.0 to 1.0 along the drawn shape.
	float distance_along_line = UV.x;

	// 2. Complex Concept Breakdown: The Sin Wave
	// This is the classic "marching ants" math. We take the position along the line, 
	// add time (scaled by speed) and put it inside a Sin wave. 
	// A Sin wave smoothly oscillates between -1.0 and 1.0. 
	float wave_value = sin((distance_along_line / dot_frequency) - (TIME * animation_speed));

	// 3. We convert that wave to a sharp on/off switch using step().
	// If the wave value is above 0.0, we draw color; otherwise, we draw transparency.
	float on_off = step(0.0, wave_value);

	// 4. Output the result
	COLOR.rgb = line_color.rgb;
	COLOR.a = line_color.a * on_off; // Apply the on/off switch to the transparency channel
}