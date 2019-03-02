import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

import { PASTRIES, getAll, del } from '../../../utils/api';

import './style.scss';
import Pastry from '../../../components/Pastry';

const PastriesView = () => {
  const [pastries, setPastries] = useState([]);
  useEffect(() => {
    fetchPastries();
  }, []);

  const handlePastryDelete = async pastryId => {
    try {
      await del(PASTRIES, pastryId);
      setPastries(pastries.filter(pastry => pastry.id !== pastryId));
    } catch (error) {
      console.log(error);
    }
  };

  const fetchPastries = async () => {
    try {
      const data = await getAll(PASTRIES);
      setPastries(data);
    } catch (error) {
      console.log(error);
    }
  };
  return (
    <div className="pastries-view">
      <Link className="link" to="/pastries/add">
        <button className="button pastries-view__add-button">
          добавить кондитерское изделий
        </button>
      </Link>
      <div className="pastries-view__pastries">
        {pastries.map(pastry => (
          <Pastry
            key={pastry.id}
            pastry={pastry}
            onDelete={handlePastryDelete}
            className="pastries-view__pastry"
          />
        ))}
      </div>
    </div>
  );
};

export default PastriesView;
