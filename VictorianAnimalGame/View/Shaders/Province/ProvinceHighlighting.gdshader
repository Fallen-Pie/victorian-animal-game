shader_type canvas_item;

uniform sampler2D province_data_map : filter_nearest;
uniform vec4 border_color : source_color = vec4(0.0, 0.0, 0.0, 1.0);
uniform float border_thickness = 0.2;

// Instead of passing a float ID, C# will now pass a Godot Color object
uniform vec4 hovered_province_color : source_color = vec4(0.0, 0.0, 0.0, 0.0);
uniform vec4 highlight_color : source_color = vec4(1.0, 1.0, 1.0, 0.3);

void fragment() {
	vec2 shift = TEXTURE_PIXEL_SIZE * border_thickness;

	// Look up the unique color from the raw image
	vec4 current_color = texture(province_data_map, UV);

	vec4 color_up    = texture(province_data_map, UV + vec2(0.0, -shift.y));
	vec4 color_down  = texture(province_data_map, UV + vec2(0.0, shift.y));
	vec4 color_left  = texture(province_data_map, UV + vec2(-shift.x, 0.0));
	vec4 color_right = texture(province_data_map, UV + vec2(shift.x, 0.0));

	vec4 final_color = texture(TEXTURE, UV);

	// To check if we are hovering, we just compare if the pixel's color matches the hovered color!
	if (distance(current_color, hovered_province_color) < 0.01) {
		final_color.rgb = mix(final_color.rgb, highlight_color.rgb, highlight_color.a);
	}

	// If our unique color doesn't match the neighbors, we are on a border
	if (current_color != color_up || current_color != color_down || current_color != color_left || current_color != color_right) {
		final_color = border_color;
	}

	COLOR = final_color;
}