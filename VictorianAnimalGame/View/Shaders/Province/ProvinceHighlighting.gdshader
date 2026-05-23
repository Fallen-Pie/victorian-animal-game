
shader_type canvas_item;

// 1. DATA INPUTS (Our textures generated from your arrays)
uniform sampler2D border_data_map : filter_nearest;
uniform sampler2D visual_data_map : filter_nearest;

// 3. BORDER & HIGHLIGHT CONFIGURATION
uniform vec4 border_color : source_color = vec4(0.0, 0.0, 0.0, 1.0);
uniform float border_thickness : hint_range(1.0, 5.0) = 1.0;
uniform vec4 hovered_province_color : source_color = vec4(0.0, 0.0, 0.0, 0.0);
uniform vec4 highlight_color : source_color = vec4(1.0, 1.0, 1.0, 0.3);

void fragment() {
	// ---- STEP 1: CHOOSE THE BACKGROUND MAP ----
	vec4 base_color = vec4(0.0);
	
	base_color = texture(visual_data_map, UV);

	// ---- STEP 2: APPLY MOUSE HOVER GLOW ----
	vec4 current_province_pixel = texture(border_data_map, UV);

	if (distance(current_province_pixel, hovered_province_color) < 0.01) {
		base_color.rgb = mix(base_color.rgb, highlight_color.rgb, highlight_color.a);
	}

	// ---- STEP 3: OVERLAY THE BORDERS ----
	// This happens LAST, meaning borders are drawn on top of ANY view mode
	vec2 shift = TEXTURE_PIXEL_SIZE * border_thickness;

	vec4 color_up    = texture(border_data_map, UV + vec2(0.0, -shift.y));
	vec4 color_down  = texture(border_data_map, UV + vec2(0.0, shift.y));
	vec4 color_left  = texture(border_data_map, UV + vec2(-shift.x, 0.0));
	vec4 color_right = texture(border_data_map, UV + vec2(shift.x, 0.0));

if (current_province_pixel != color_up || current_province_pixel != color_down || current_province_pixel != color_left || current_province_pixel != color_right) {
	base_color = border_color;
}

// ---- STEP 4: OUTPUT TO SCREEN ----
COLOR = base_color;
}