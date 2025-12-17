// Основная логика приложения
document.addEventListener('DOMContentLoaded', function() {
    // Инициализация активного пункта меню
    initActiveMenu();
    
    // Инициализация кнопок
    initButtons();
    
    // Инициализация выпадающих меню
    initDropdowns();
    
    // Инициализация модальных окон
    initModals();
});

// Инициализация активного пункта меню
function initActiveMenu() {
    const currentPage = window.location.pathname.split('/').pop() || 'index.html';
    const navItems = document.querySelectorAll('.nav-item');
    
    navItems.forEach(item => {
        const href = item.getAttribute('href');
        if (href === currentPage) {
            item.classList.add('active');
        } else {
            item.classList.remove('active');
        }
    });
}

// Инициализация кнопок
function initButtons() {
    // Кнопка загрузки звонка
    const uploadBtn = document.querySelector('.btn-upload');
    if (uploadBtn) {
        uploadBtn.addEventListener('click', function() {
            showNotification('Загрузка аудиофайла', 'Открыт диалог выбора файла для анализа', 'info');
        });
    }
    
    // Кнопка AI-анализа
    const aiAnalysisBtn = document.querySelector('.btn-ai-analysis');
    if (aiAnalysisBtn) {
        aiAnalysisBtn.addEventListener('click', function() {
            showNotification('AI-анализ', 'Запущен анализ выбранного звонка', 'success');
        });
    }
    
    // Кнопка нового анализа
    //const newAnalysisBtn = document.querySelector('.btn-new-analysis');
    //if (newAnalysisBtn) {
    //    newAnalysisBtn.addEventListener('click', function() {
    //        showNotification('Новый анализ', 'Создан новый анализ звонков', 'info');
    //    });
    //}
}

// Инициализация выпадающих меню
function initDropdowns() {
    const dropdowns = document.querySelectorAll('.dropdown');
    
    dropdowns.forEach(dropdown => {
        const toggle = dropdown.querySelector('.dropdown-toggle');
        const menu = dropdown.querySelector('.dropdown-menu');
        
        if (toggle && menu) {
            toggle.addEventListener('click', function(e) {
                e.stopPropagation();
                menu.classList.toggle('show');
            });
        }
    });
    
    // Закрытие выпадающих меню при клике вне
    document.addEventListener('click', function() {
        document.querySelectorAll('.dropdown-menu').forEach(menu => {
            menu.classList.remove('show');
        });
    });
}

// Инициализация модальных окон
function initModals() {
    const modalTriggers = document.querySelectorAll('[data-modal]');
    
    modalTriggers.forEach(trigger => {
        trigger.addEventListener('click', function() {
            const modalId = this.getAttribute('data-modal');
            const modal = document.getElementById(modalId);
            
            if (modal) {
                modal.classList.add('show');
                document.body.style.overflow = 'hidden';
            }
        });
    });
    
    // Закрытие модальных окон
    const closeButtons = document.querySelectorAll('.modal-close, .modal .btn-close');
    
    closeButtons.forEach(button => {
        button.addEventListener('click', function() {
            const modal = this.closest('.modal');
            if (modal) {
                modal.classList.remove('show');
                document.body.style.overflow = 'auto';
            }
        });
    });
    
    // Закрытие по клику на фон
    document.addEventListener('click', function(e) {
        if (e.target.classList.contains('modal')) {
            e.target.classList.remove('show');
            document.body.style.overflow = 'auto';
        }
    });
}

// Показать уведомление
function showNotification(title, message, type = 'info') {
    // В реальном приложении можно использовать библиотеку для уведомлений
    console.log(`[${type.toUpperCase()}] ${title}: ${message}`);
    
    // Создаем простое уведомление
    const notification = document.createElement('div');
    notification.className = `notification notification-${type}`;
    notification.innerHTML = `
        <strong>${title}</strong>
        <p>${message}</p>
    `;
    
    notification.style.cssText = `
        position: fixed;
        top: 20px;
        right: 20px;
        padding: 1rem;
        background-color: white;
        border-left: 4px solid ${getNotificationColor(type)};
        border-radius: 8px;
        box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        z-index: 1000;
        max-width: 300px;
        animation: slideIn 0.3s ease;
    `;
    
    document.body.appendChild(notification);
    
    // Удаляем уведомление через 5 секунд
    setTimeout(() => {
        notification.style.animation = 'slideOut 0.3s ease';
        setTimeout(() => {
            if (notification.parentNode) {
                notification.parentNode.removeChild(notification);
            }
        }, 300);
    }, 5000);
    
    // Добавляем стили для анимации
    if (!document.querySelector('#notification-styles')) {
        const style = document.createElement('style');
        style.id = 'notification-styles';
        style.textContent = `
            @keyframes slideIn {
                from { transform: translateX(100%); opacity: 0; }
                to { transform: translateX(0); opacity: 1; }
            }
            @keyframes slideOut {
                from { transform: translateX(0); opacity: 1; }
                to { transform: translateX(100%); opacity: 0; }
            }
        `;
        document.head.appendChild(style);
    }
}

// Цвет уведомления по типу
function getNotificationColor(type) {
    const colors = {
        'success': '#10b981',
        'info': '#3b82f6',
        'warning': '#f59e0b',
        'error': '#ef4444'
    };
    return colors[type] || colors.info;
}