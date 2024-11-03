// utils/auth.ts


export const saveToLocalStorage = (user: User) => {
    localStorage.setItem('user', JSON.stringify(user));
};

export const removeFromLocalStorage = () => {
    localStorage.removeItem('user');
};

export const loadFromLocalStorage = () => {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
};