extends TileMapLayer

var platform_speed = 100 # Скорость генерации платформ
var platform_gap = 100  # Зазор между платформами

var platforms = []   # Массив для хранения платформ
var last_platform_y = 0  # Позиция Y последней платформы

func _physics_process(delta):
	# Генерация платформ
	generate_platforms(delta)

func generate_platforms(delta):
	# Проверяем, нужно ли генерировать новую платформу
	if position.y > last_platform_y + platform_gap:
		# Создаем новую платформу
		var platform = load("res://node_2d.tscn").instance()  # Замените "res://platform.tscn" на путь к сцене платформы
		add_child(platform)

		# Устанавливаем случайное положение платформы по X
		platform.position.x = randf_range(0, get_viewport().size.x - platform.get_node("Sprite").texture.get_size().x)

		# Устанавливаем положение платформы по Y
		platform.position.y = position.y + platform_gap

		# Добавляем платформу в массив
		platforms.append(platform)

		# Обновляем позицию последней платформы
		last_platform_y = position.y

	# Проверяем, нужно ли удалить платформы за пределами экрана
	for platform in platforms:
		if platform.position.y < position.y - get_viewport().size.y:
			remove_child(platform)
			platforms.erase(platform)
