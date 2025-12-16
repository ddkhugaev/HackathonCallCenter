// График эффективности рекомендаций
document.addEventListener('DOMContentLoaded', function() {
    const ctx = document.getElementById('recommendationsChart').getContext('2d');
    
    new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн'],
            datasets: [
                {
                    label: 'Принятые рекомендации',
                    data: [8, 12, 15, 18, 22, 25],
                    borderColor: 'rgb(16, 185, 129)',
                    backgroundColor: 'rgba(16, 185, 129, 0.1)',
                    borderWidth: 2,
                    tension: 0.4,
                    fill: true
                },
                {
                    label: 'Рост эффективности (%)',
                    data: [5, 8, 12, 15, 18, 23],
                    borderColor: 'rgb(37, 99, 235)',
                    backgroundColor: 'rgba(37, 99, 235, 0.1)',
                    borderWidth: 2,
                    tension: 0.4,
                    fill: true,
                    yAxisID: 'y1'
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Количество рекомендаций'
                    }
                },
                y1: {
                    position: 'right',
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Рост эффективности (%)'
                    },
                    grid: {
                        drawOnChartArea: false
                    }
                }
            }
        }
    });
    
    // Обработка кнопок рекомендаций
    document.querySelectorAll('.recommendation-item .btn-icon').forEach(btn => {
        btn.addEventListener('click', function() {
            const title = this.getAttribute('title');
            const recommendation = this.closest('.recommendation-item').querySelector('div[style*="font-weight: 600"]').textContent;
            
            alert(`Действие: ${title}\nРекомендация: ${recommendation}`);
        });
    });
    
    // Обработка кнопок принять/отклонить
    document.querySelectorAll('.btn-success, .btn-secondary, .btn-primary').forEach(btn => {
        if (btn.textContent.includes('Принять') || btn.textContent.includes('Отклонить') || btn.textContent.includes('Отложить')) {
            btn.addEventListener('click', function() {
                const action = this.textContent.trim();
                const recommendation = this.closest('.ai-card').querySelector('.ai-title').textContent.trim();
                
                alert(`Рекомендация: ${recommendation}\nДействие: ${action}`);
            });
        }
    });
});