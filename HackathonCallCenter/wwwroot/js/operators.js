// Графики для страницы операторов
document.addEventListener('DOMContentLoaded', function() {
    // График успешности операторов
    const successCtx = document.getElementById('operatorsSuccessChart').getContext('2d');
    new Chart(successCtx, {
        type: 'bar',
        data: {
            labels: ['Анна', 'Мария', 'Ольга', 'Леон', 'Иван'],
            datasets: [{
                label: 'Успешность сделок (%)',
                data: [94, 91, 89, 87, 83],
                backgroundColor: [
                    'rgba(37, 99, 235, 0.7)',
                    'rgba(139, 92, 246, 0.7)',
                    'rgba(236, 72, 153, 0.7)',
                    'rgba(16, 185, 129, 0.7)',
                    'rgba(245, 158, 11, 0.7)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    max: 100,
                    title: {
                        display: true,
                        text: 'Успешность (%)'
                    }
                }
            }
        }
    });
    
    // График длительности звонков
    const durationCtx = document.getElementById('operatorsDurationChart').getContext('2d');
    new Chart(durationCtx, {
        type: 'radar',
        data: {
            labels: ['Приветствие', 'Выявление потребности', 'Предложение', 'Работа с возражениями', 'Завершение', 'Прощание'],
            datasets: [
                {
                    label: 'Анна',
                    data: [9, 8, 8, 7, 9, 9],
                    borderColor: 'rgb(37, 99, 235)',
                    backgroundColor: 'rgba(37, 99, 235, 0.2)',
                    borderWidth: 2
                },
                {
                    label: 'Леон',
                    data: [7, 8, 9, 6, 7, 8],
                    borderColor: 'rgb(16, 185, 129)',
                    backgroundColor: 'rgba(16, 185, 129, 0.2)',
                    borderWidth: 2
                },
                {
                    label: 'Иван',
                    data: [8, 7, 6, 5, 7, 8],
                    borderColor: 'rgb(245, 158, 11)',
                    backgroundColor: 'rgba(245, 158, 11, 0.2)',
                    borderWidth: 2
                }
            ]
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
    
    // Обработка кнопок операторов
    document.querySelectorAll('.operator-item .btn-icon').forEach(btn => {
        btn.addEventListener('click', function() {
            const title = this.getAttribute('title');
            const operatorItem = this.closest('.operator-item');
            const operatorName = operatorItem.querySelector('.operator-name').textContent;
            
            alert(`Действие: ${title}\nОператор: ${operatorName}`);
        });
    });
});