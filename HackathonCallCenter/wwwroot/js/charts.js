// Инициализация графиков с Chart.js
document.addEventListener('DOMContentLoaded', function() {
    // Проверяем, есть ли на странице контейнеры для графиков
    if (document.getElementById('callsChart')) {
        initCallsChart();
    }
    
    if (document.getElementById('successRateChart')) {
        initSuccessRateChart();
    }
    
    if (document.getElementById('satisfactionChart')) {
        initSatisfactionChart();
    }
    
    if (document.getElementById('operatorsChart')) {
        initOperatorsChart();
    }
    
    if (document.getElementById('emotionsChart')) {
        initEmotionsChart();
    }
    
    if (document.getElementById('topicsChart')) {
        initTopicsChart();
    }
});

// График количества звонков по дням
function initCallsChart() {
    const ctx = document.getElementById('callsChart').getContext('2d');
    
    const data = {
        labels: ['Пн', 'Вт', 'Ср', 'Чт', 'Пт', 'Сб', 'Вс'],
        datasets: [{
            label: 'Входящие звонки',
            data: [45, 52, 48, 61, 58, 32, 28],
            backgroundColor: 'rgba(37, 99, 235, 0.2)',
            borderColor: 'rgb(37, 99, 235)',
            borderWidth: 2,
            tension: 0.4,
            fill: true
        }, {
            label: 'Исходящие звонки',
            data: [28, 32, 35, 40, 38, 25, 20],
            backgroundColor: 'rgba(16, 185, 129, 0.2)',
            borderColor: 'rgb(16, 185, 129)',
            borderWidth: 2,
            tension: 0.4,
            fill: true
        }]
    };
    
    new Chart(ctx, {
        type: 'line',
        data: data,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'top',
                },
                tooltip: {
                    mode: 'index',
                    intersect: false
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Количество звонков'
                    }
                }
            }
        }
    });
}

// График успешности сделок
function initSuccessRateChart() {
    const ctx = document.getElementById('successRateChart').getContext('2d');
    
    const data = {
        labels: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн'],
        datasets: [{
            label: 'Успешные сделки',
            data: [82, 85, 87, 89, 90, 92],
            backgroundColor: 'rgba(16, 185, 129, 0.5)',
            borderColor: 'rgb(16, 185, 129)',
            borderWidth: 2
        }, {
            label: 'Неуспешные сделки',
            data: [18, 15, 13, 11, 10, 8],
            backgroundColor: 'rgba(239, 68, 68, 0.5)',
            borderColor: 'rgb(239, 68, 68)',
            borderWidth: 2
        }]
    };
    
    new Chart(ctx, {
        type: 'bar',
        data: data,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'top',
                }
            },
            scales: {
                x: {
                    stacked: true,
                },
                y: {
                    stacked: true,
                    beginAtZero: true,
                    max: 100,
                    title: {
                        display: true,
                        text: 'Процент (%)'
                    }
                }
            }
        }
    });
}

// График удовлетворенности клиентов
function initSatisfactionChart() {
    const ctx = document.getElementById('satisfactionChart').getContext('2d');
    
    new Chart(ctx, {
        type: 'doughnut',
        data: {
            labels: ['Высокая', 'Средняя', 'Низкая'],
            datasets: [{
                data: [65, 25, 10],
                backgroundColor: [
                    'rgb(16, 185, 129)',
                    'rgb(245, 158, 11)',
                    'rgb(239, 68, 68)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'top',
                }
            }
        }
    });
}

// График производительности операторов
function initOperatorsChart() {
    const ctx = document.getElementById('operatorsChart').getContext('2d');
    
    const data = {
        labels: ['Анна', 'Леон', 'Мария', 'Иван', 'Ольга'],
        datasets: [{
            label: 'Успешность сделок (%)',
            data: [94, 87, 91, 83, 89],
            backgroundColor: 'rgba(37, 99, 235, 0.5)',
            borderColor: 'rgb(37, 99, 235)',
            borderWidth: 2
        }, {
            label: 'Удовлетворенность клиентов',
            data: [8.7, 7.9, 8.5, 7.8, 8.2],
            backgroundColor: 'rgba(16, 185, 129, 0.5)',
            borderColor: 'rgb(16, 185, 129)',
            borderWidth: 2,
            yAxisID: 'y1'
        }]
    };
    
    new Chart(ctx, {
        type: 'bar',
        data: data,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'top',
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    max: 100,
                    title: {
                        display: true,
                        text: 'Успешность (%)'
                    }
                },
                y1: {
                    position: 'right',
                    beginAtZero: true,
                    max: 10,
                    title: {
                        display: true,
                        text: 'Удовлетворенность'
                    },
                    grid: {
                        drawOnChartArea: false
                    }
                }
            }
        }
    });
}

// График анализа эмоций
function initEmotionsChart() {
    const ctx = document.getElementById('emotionsChart').getContext('2d');
    
    const data = {
        labels: ['Позитивные', 'Нейтральные', 'Негативные', 'Смешанные'],
        datasets: [{
            label: 'Распределение эмоций',
            data: [42, 35, 15, 8],
            backgroundColor: [
                'rgba(16, 185, 129, 0.7)',
                'rgba(245, 158, 11, 0.7)',
                'rgba(239, 68, 68, 0.7)',
                'rgba(139, 92, 246, 0.7)'
            ],
            borderWidth: 1
        }]
    };
    
    new Chart(ctx, {
        type: 'pie',
        data: data,
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'top',
                }
            }
        }
    });
}

// График популярных тем
function initTopicsChart() {
    const ctx = document.getElementById('topicsChart').getContext('2d');
    
    new Chart(ctx, {
        type: 'horizontalBar',
        data: {
            labels: ['Цены и тарифы', 'Технические вопросы', 'Оформление заказа', 'Качество обслуживания', 'Доставка', 'Гарантия'],
            datasets: [{
                label: 'Количество упоминаний',
                data: [45, 38, 32, 28, 25, 18],
                backgroundColor: 'rgba(37, 99, 235, 0.7)',
                borderColor: 'rgb(37, 99, 235)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            indexAxis: 'y',
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                x: {
                    beginAtZero: true
                }
            }
        }
    });
}