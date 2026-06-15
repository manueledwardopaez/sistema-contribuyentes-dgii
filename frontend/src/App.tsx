import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import TaxpayerList from './components/TaxpayerList';
import TaxpayerDetail from './components/TaxpayerDetail';

const App: React.FC = () => {
  return (
    <Router>
      <div className="app-container">
        <Routes>
          <Route path="/" element={<TaxpayerList />} />
          <Route path="/taxpayer/:rncCedula" element={<TaxpayerDetail />} />
        </Routes>
      </div>
    </Router>
  );
};

export default App;
