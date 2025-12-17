// Навигация между страницами
document.addEventListener('DOMContentLoaded', function() {
    // Инициализация ссылок меню
    initMenuLinks();
    
    // Инициализация кнопок назад
    initBackButtons();
});

// Инициализация ссылок меню
function initMenuLinks() {
    const menuLinks = document.querySelectorAll('.nav-item');
    
    menuLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            if (this.getAttribute('href').startsWith('#')) {
                e.preventDefault();
                return;
            }
            
            // В реальном приложении здесь будет переход на другую страницу
            // Для демо просто показываем уведомление
            const pageName = this.querySelector('span').textContent;
            showNotification('Переход', `Загрузка страницы: ${pageName}`, 'info');
        });
    });
}

// Инициализация кнопок назад
function initBackButtons() {
    const backButtons = document.querySelectorAll('.btn-back');
    
    backButtons.forEach(button => {
        button.addEventListener('click', function() {
            window.history.back();
        });
    });
}

// Загрузка данных операторов
async function loadOperatorsData() {
    try {
        // В реальном приложении будет fetch запрос
        // Для демо используем моковые данные
        const mockData = [
            { id: 1, name: "Анна", phone: "+7 927 368 99 93", successRate: 94, satisfaction: 8.7, calls: 127 },
            { id: 2, name: "Леон", phone: "+963 179 88 77", successRate: 87, satisfaction: 7.9, calls: 98 },
            { id: 3, name: "Мария", phone: "+7 916 123 45 67", successRate: 91, satisfaction: 8.5, calls: 112 },
            { id: 4, name: "Иван", phone: "+7 925 987 65 43", successRate: 83, satisfaction: 7.8, calls: 89 },
            { id: 5, name: "Ольга", phone: "+7 903 456 78 90", successRate: 89, satisfaction: 8.2, calls: 105 }
        ];
        
        return mockData;
    } catch (error) {
        console.error('Ошибка загрузки данных операторов:', error);
        return [];
    }
}

// Загрузка данных звонков
async function loadCallsData() {
    try {
        const mockData = [
            { id: 1, number: "+7 927 368 99 93", type: "incoming", duration: "4:18", date: "Сегодня, 10:24", success: true },
            { id: 2, number: "+963 179 88 77", type: "outgoing", duration: "7:02", date: "Вчера, 16:42", success: true },
            { id: 3, number: "+7 910 555 43 21", type: "incoming", duration: "2:50", date: "Вчера, 14:15", success: false },
            { id: 4, number: "+7 495 123 45 67", type: "missed", duration: "0:00", date: "Позавчера, 11:30", success: false },
            { id: 5, number: "+7 916 789 12 34", type: "incoming", duration: "5:45", date: "Позавчера, 09:15", success: true }
        ];
        
        return mockData;
    } catch (error) {
        console.error('Ошибка загрузки данных звонков:', error);
        return [];
    }
}