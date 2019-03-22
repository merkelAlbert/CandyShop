import axios from 'axios';

const BASE_URL = 'http://localhost:5000/api';
export const USERS = BASE_URL + '/users';
export const PASTRIES = BASE_URL + '/pastries';
export const ORDERS = BASE_URL + '/orders';

export const getAll = async (url, params) => {
  const options = {
    params
  };
  const { data } = await axios.get(url, options);
  return data;
};
export const getOne = async (url, id) => {
  const { data } = await axios.get(`${url}/${id}`);
  return data;
};
export const post = async (url, payload) => {
  const { data } = await axios.post(url, payload);
  return data;
};
export const put = async (url, id, payload) => {
  const { data } = await axios.put(`${url}/${id}`, payload);
  return data;
};
export const del = async (url, id) => {
  const { data } = await axios.delete(`${url}/${id}`);
  return data;
};
