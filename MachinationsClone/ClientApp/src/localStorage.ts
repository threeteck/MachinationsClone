import {User} from "./common/types";

export class LocalStorage {
    static get(key: string): any {
        return JSON.parse(localStorage.getItem(key) || 'null');
    }
    
    static getString(key: string): string {
        return localStorage.getItem(key) || '';
    }
    
    static set(key: string, value: any): void {
        const json = JSON.stringify(value);
        localStorage.setItem(key, json);
    }
    
    static setString(key: string, value: string): void {
        localStorage.setItem(key, value);
    }
    
    static remove(key: string): void {
        localStorage.removeItem(key);
    }
    
    static clear(): void {
        localStorage.clear();
    }
}

export class Auth {
    static getUser(): User {
        return LocalStorage.get('user');
    }
    
    static setUser(user: User): void {
        LocalStorage.set('user', user);
    }

    static getToken(): string {
        return LocalStorage.getString('token');
    }

    static setToken(token: string): void {
        LocalStorage.setString('token', token);
    }
    
    static removeUser(): void {
        LocalStorage.remove('user');
        LocalStorage.remove('token');
    }
    
    static isLoggedIn(): boolean {
        return !!Auth.getUser() && !!Auth.getToken();
    }
}
