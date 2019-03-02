import React, { useState, useEffect } from 'react';
import { withRouter } from 'react-router-dom';

import { PASTRIES, post, getOne, put } from '../../../utils/api';

import './style.scss';

const types = [
  { value: 0, title: 'Торт' },
  { value: 1, title: 'Кекс' },
  { value: 2, title: 'Печенье' }
];

const PastriesForm = ({
  history,
  match: {
    params: { pastryId }
  }
}) => {
  const [name, setName] = useState('');
  const [pastryType, setPastryType] = useState(0);
  const [description, setDescription] = useState('');
  const [price, setPrice] = useState('');
  const [compound, setCompound] = useState('');

  useEffect(() => {
    if (pastryId) {
      fetchPastry();
    }
  }, []);

  const fetchPastry = async () => {
    try {
      const data = await getOne(PASTRIES, pastryId);
      setName(data.name);
      setPastryType(data.pastryType);
      setDescription(data.description);
      setPrice(data.price);
      setCompound(data.compound);
    } catch (error) {
      console.log(error);
    }
  };

  const handleChange = e => {
    switch (e.target.name) {
      case 'name':
        setName(e.target.value);
        break;
      case 'description':
        setDescription(e.target.value);
        break;
      case 'pastryType':
        setPastryType(e.target.value);
        break;
      case 'price':
        setPrice(e.target.value);
        break;
      case 'compound':
        setCompound(e.target.value);
        break;
      default: {
      }
    }
  };

  const handleSubmit = async e => {
    e.preventDefault();
    const payload = {
      name,
      description,
      pastryType,
      price,
      compound
    };
    try {
      if (!pastryId) await post(PASTRIES, payload);
      else await put(PASTRIES, pastryId, payload);
      history.push('/pastries');
    } catch (error) {
      console.log(error);
    }
  };

  return (
    <form className="pastries-form" onSubmit={handleSubmit}>
      <input
        className="text pastries-form__field"
        type="text"
        name="name"
        placeholder="Название"
        value={name}
        onChange={handleChange}
      />
      <select
        className="text pastries-form__field"
        name="pastryType"
        placeholder="Тип"
        value={pastryType}
        onChange={handleChange}
      >
        {types.map(type => (
          <option key={type.value} value={type.value}>
            {type.title}
          </option>
        ))}
      </select>
      <input
        className="text pastries-form__field"
        type="text"
        name="description"
        placeholder="Описание"
        value={description}
        onChange={handleChange}
      />
      <input
        className="text pastries-form__field"
        type="number"
        name="price"
        placeholder="Цена"
        value={price}
        onChange={handleChange}
      />
      <input
        className="text pastries-form__field"
        type="text"
        name="compound"
        placeholder="Состав"
        value={compound}
        onChange={handleChange}
      />
      <input
        className="button pastries-form__submit-button"
        type="submit"
        value={!pastryId ? 'Добавить' : 'Изменить'}
      />
    </form>
  );
};

export default withRouter(PastriesForm);
