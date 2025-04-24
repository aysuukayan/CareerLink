import axios from "axios";

const API_BASE = "https://localhost:5001/api";

axios.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem("token");
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => Promise.reject(error)
);

export const getJobs = () => axios.get(`${API_BASE}/jobs`);

export const login = (data) => axios.post(`${API_BASE}/login`, data);

export const register = (data) => axios.post(`${API_BASE}/register`, data);

export const uploadCV = (formData) =>
  axios.post(`${API_BASE}/upload-cv`, formData, {
    headers: { "Content-Type": "multipart/form-data" }
  });

export const getRecommendations = (userId) =>
  axios.get(`${API_BASE}/recommendations?userId=${userId}`);

export const getUserProfile = () =>
  axios.get(`${API_BASE}/user/profile`);

export const updateUserProfile = (data) =>
  axios.put(`${API_BASE}/user/profile`, data);
