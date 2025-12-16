// Графики для страницы выявления проблем
document.addEventListener('DOMContentLoaded', function() {
    // График распределения проблем по типам
    const typeCtx = document.getElementById('problemsByTypeChart').getContext('2d');
    new Chart(typeCtx, {
        type: 'doughnut',
        data: {
            labels: ['Технические', 'Скрипты', 'Обучение', 'Эмоции', 'Сервис', 'Другие'],
            datasets: [{
                data: [25, 18, 22, 15, 12, 8],
                backgroundColor: [
                    'rgb(37, 99, 235)',
                    'rgb(239, 68, 68)',
                    'rgb(245, 158, 11)',
                    'rgb(16, 185, 129)',
                    'rgb(139, 92, 246)',
                    'rgb(107, 114, 128)'
                ]
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
    
    // График динамики проблем
    const trendCtx = document.getElementById('problemsTrendChart').getContext('2d');
    new Chart(trendCtx, {
        type: 'line',
        data: {
            labels: ['Янв', 'Фев', 'Мар', 'Апр', 'Май', 'Июн'],
            datasets: [
                {
                    label: 'Обнаруженные проблемы',
                    data: [35, 42, 38, 45, 52, 48],
                    borderColor: 'rgb(239, 68, 68)',
                    backgroundColor: 'rgba(239, 68, 68, 0.1)',
                    borderWidth: 2,
                    tension: 0.4,
                    fill: true
                },
                {
                    label: 'Решенные проблемы',
                    data: [25, 30, 28, 32, 38, 42],
                    borderColor: 'rgb(16, 185, 129)',
                    backgroundColor: 'rgba(16, 185, 129, 0.1)',
                    borderWidth: 2,
                    tension: 0.4,
                    fill: true
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false
        }
    });
    
    // Обработка кнопок в таблице
    document.querySelectorAll('.data-table .btn-icon').forEach(btn => {
        btn.addEventListener('click', function() {
            const title = this.getAttribute('title');
            const row = this.closest('tr');
            const problemId = row.querySelector('td:first-child').textContent;
            const problemType = row.querySelector('td:nth-child(2)').textContent;
            
            alert(`Действие: ${title}\nПроблема: ${problemId}\nТип: ${problemType}`);
        });
    });
    
    // Обработка кнопок критических проблем
    document.querySelectorAll('.ai-card .btn-danger, .ai-card .btn-secondary').forEach(btn => {
        btn.addEventListener('click', function() {
            const action = this.textContent.trim();
            const problem = this.closest('.ai-card').querySelector('.ai-title').textContent.trim();
            
            alert(`Проблема: ${problem}\nДействие: ${action}`);
        });
    });
});