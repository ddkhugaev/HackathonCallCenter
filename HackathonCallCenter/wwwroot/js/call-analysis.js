// call-analysis.js
document.addEventListener('DOMContentLoaded', function () {
    // Логика для страницы анализа разговора

    // Кнопка прослушивания
    const listenBtn = document.querySelector('.btn-play-audio');
    if (listenBtn) {
        listenBtn.addEventListener('click', function () {
            alert('Воспроизведение аудиозаписи...');
            // Здесь будет логика воспроизведения аудио
        });
    }

    // Кнопка экспорта
    const exportBtn = document.querySelector('.btn-export');
    if (exportBtn) {
        exportBtn.addEventListener('click', function () {
            alert('Отчет экспортируется...');
            // Здесь будет логика экспорта
        });
    }

    // Инициализация всех графиков
    initializeCharts();
});

function initializeCharts() {
    // Графики уже инициализированы в inline скрипте в View
    // Можно добавить дополнительную логику
    console.log('Графики анализа разговора инициализированы');
}