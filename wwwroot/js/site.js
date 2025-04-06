document.addEventListener('DOMContentLoaded', function () {
    // Đóng modal
    const closeBtn = document.getElementById('closeAddUserBtn');
    if (closeBtn) {
        closeBtn.addEventListener('click', () => {
            document.getElementById('addUserModal').classList.add('hidden');
        });
    }

    // Xử lý xóa học viên
    document.querySelectorAll('.remove-user-btn').forEach(button => {
        button.addEventListener('click', function () {
            const userId = this.closest('[data-user-id]').getAttribute('data-user-id');
            const courseId = document.querySelector('input[name="courseId"]').value;
            removeUser(userId, courseId, this);
        });
    });

    // Kích hoạt nút "Thêm học viên"
    const checkboxes = document.querySelectorAll('input[name="userIds"]');
    const addButton = document.getElementById('addButton');

    const toggleAddButton = () => {
        const checkedCount = document.querySelectorAll('input[name="userIds"]:checked').length;
        addButton.disabled = checkedCount === 0;
    };

    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', toggleAddButton);
    });

    // Gọi toggle ban đầu
    toggleAddButton();
});

// Hàm xóa học viên
function removeUser(userId, courseId, buttonElement) {
    if (confirm('Bạn có chắc chắn muốn xóa học viên này khỏi khóa học?')) {
        fetch('/Course/RemoveUserFromCourse', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ userId, courseId })
        })
            .then(response => response.json())
            .then(data => {
                if (data.success) {
                    const userElement = buttonElement.closest(`[data-user-id="${userId}"]`);
                    if (userElement) userElement.remove();

                    const registeredUsersList = document.getElementById('registeredUsersList');
                    if (!registeredUsersList.querySelector('.flex')) {
                        registeredUsersList.innerHTML = '<p class="text-gray-500 italic">Chưa có học viên nào đăng ký.</p>';
                    }
                } else {
                    alert(data.message || 'Có lỗi xảy ra khi xóa học viên.');
                }
            })
            .catch(error => {
                console.error('Error:', error);
                alert('Có lỗi xảy ra khi xóa học viên.');
            });
    }
}