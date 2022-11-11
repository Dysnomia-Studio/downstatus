import React, { Component } from 'react';
import { Route, Routes } from 'react-router-dom';

import Home from './components/Home';
import Layout from './components/Layout';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Routes>
                    <Route key="/" index={true} element={<Home />} />
                </Routes>
            </Layout>
        );
    }
}
