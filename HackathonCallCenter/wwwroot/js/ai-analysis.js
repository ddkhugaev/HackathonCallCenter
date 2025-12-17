        // Имитация взаимодействия с интерфейсом
        document.addEventListener('DOMContentLoaded', function() {
            // Переключение активного звонка в списке
            const callItems = document.querySelectorAll('.call-item');
            callItems.forEach(item => {
                item.addEventListener('click', function() {
                    callItems.forEach(i => i.classList.remove('active'));
                    this.classList.add('active');
                    
                    // Здесь в реальном приложении загружались бы данные выбранного звонка
                    console.log('Выбран звонок:', this.querySelector('.call-number').textContent);
                });
            });
            
            // Обработка кнопки AI-анализ
            const aiAnalysisBtn = document.querySelector('.btn-primary');
            aiAnalysisBtn.addEventListener('click', function() {
                alert('Запущен AI-анализ выбранного звонка. Результаты будут обновлены в правой панели.');
            });
            
            // Обработка кнопки загрузки звонка
            const uploadBtn = document.querySelector('.btn-secondary');
            uploadBtn.addEventListener('click', function() {
                alert('Открыт диалог выбора аудиофайла для загрузки и анализа.');
            });
        });