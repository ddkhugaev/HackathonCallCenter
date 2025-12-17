document.addEventListener('DOMContentLoaded', function() {
    // График распределения звонков по времени суток
    const timeCtx = document.getElementById('timeDistributionChart').getContext('2d');
    new Chart(timeCtx, {
        type: 'line',
        data: {
            labels: ['00:00', '04:00', '08:00', '12:00', '16:00', '20:00', '23:59'],
            datasets: [{
                label: 'Количество звонков',
                data: [5, 8, 45, 68, 52, 35, 12],
                backgroundColor: 'rgba(37, 99, 235, 0.1)',
                borderColor: 'rgb(37, 99, 235)',
                borderWidth: 2,
                tension: 0.4,
                fill: true
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
    
    // График длительности звонков
    const durationCtx = document.getElementById('durationChart').getContext('2d');
    new Chart(durationCtx, {
        type: 'bar',
        data: {
            labels: ['<1 мин', '1-3 мин', '3-5 мин', '5-10 мин', '10-15 мин', '>15 мин'],
            datasets: [{
                label: 'Количество звонков',
                data: [25, 68, 142, 89, 45, 12],
                backgroundColor: 'rgba(16, 185, 129, 0.7)',
                borderColor: 'rgb(16, 185, 129)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
    
    // График типов обращений
    const requestCtx = document.getElementById('requestTypesChart').getContext('2d');
    new Chart(requestCtx, {
        type: 'doughnut',
        data: {
            labels: ['Консультация', 'Жалоба', 'Заказ', 'Техподдержка', 'Другое'],
            datasets: [{
                data: [35, 15, 25, 18, 7],
                backgroundColor: [
                    'rgb(37, 99, 235)',
                    'rgb(239, 68, 68)',
                    'rgb(16, 185, 129)',
                    'rgb(245, 158, 11)',
                    'rgb(139, 92, 246)'
                ]
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
    
    // График эффективности сценариев
    const scenariosCtx = document.getElementById('scenariosChart').getContext('2d');
    new Chart(scenariosCtx, {
        type: 'radar',
        data: {
            labels: ['Приветствие', 'Выявление потребности', 'Предложение решения', 'Работа с возражениями', 'Завершение сделки', 'Прощание'],
            datasets: [{
                label: 'Эффективность',
                data: [9, 8, 7, 6, 8, 9],
                backgroundColor: 'rgba(37, 99, 235, 0.2)',
                borderColor: 'rgb(37, 99, 235)',
                borderWidth: 2
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                r: {
                    beginAtZero: true,
                    max: 10
                }
            }
        }
    });
    
    // Обработка фильтров
    document.querySelectorAll('.filter-btn').forEach(btn => {
        btn.addEventListener('click', function() {
            document.querySelectorAll('.filter-btn').forEach(b => b.classList.remove('active'));
            this.classList.add('active');
            showNotification('Фильтр', `Применен фильтр: ${this.textContent}`, 'info');
        });
    });
});