// График активности звонков
document.addEventListener('DOMContentLoaded', function() {
    const ctx = document.getElementById('callsActivityChart').getContext('2d');
    
    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: ['00:00', '02:00', '04:00', '06:00', '08:00', '10:00', '12:00', '14:00', '16:00', '18:00', '20:00', '22:00'],
            datasets: [{
                label: 'Количество звонков',
                data: [3, 1, 0, 2, 15, 42, 68, 54, 48, 32, 18, 8],
                backgroundColor: 'rgba(37, 99, 235, 0.7)',
                borderColor: 'rgb(37, 99, 235)',
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Количество звонков'
                    }
                },
                x: {
                    title: {
                        display: true,
                        text: 'Время суток'
                    }
                }
            }
        }
    });
    
    // Обработка кнопок в таблице
    document.querySelectorAll('.btn-icon').forEach(btn => {
        btn.addEventListener('click', function() {
            const title = this.getAttribute('title');
            const row = this.closest('tr');
            const callId = row.querySelector('td:first-child').textContent;
            const phone = row.querySelector('td:nth-child(2)').textContent;
            
            alert(`Действие: ${title}\nЗвонок: ${callId}\nНомер: ${phone}`);
        });
    });
});