import React from 'react';
import Shackmeet from '../shackmeet/Shackmeet';

class Home extends React.Component {
  displayName = Home.name

  constructor(props) {
    super(props);
    this.state = { meets: [], loading: true };

    fetch('api/GetShackmeets')
      .then(response => response.json())
      .then(data => {
        this.setState({ meets: data, loading: false });
      });
  }

  static renderShackmeets(meets) {
    return (
      <div>
        {meets.map(meet =>
          <Shackmeet meet={meet} />
        )}
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Home.renderShackmeets(this.state.meets);

    return (
      <div>
        <h1>Upcoming Shackmeets</h1>
        {contents}
      </div>
    );
  }
}

export default Home;