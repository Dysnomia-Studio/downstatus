import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';

import Details from './components/Details';
import Home from './components/Home';
import Layout from './components/Layout';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Routes>
                    <Route path="/details/:key" element={<Details />} />
                    <Route path="/" index={true} element={<Home />} />
                </Routes>
            </Layout>
        );
    }
}
