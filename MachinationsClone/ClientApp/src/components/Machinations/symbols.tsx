﻿export const symbols: Record<string, JSX.Element> = {
  pool: (
    <div className="m-generic-shape-svg">
      <svg
        xmlns="http://www.w3.org/2000/svg"
        width="32"
        height="32"
        style={{
          left: '0px',
          top: '0px',
          width: '100%',
          height: '100%',
          display: 'block',
          minWidth: '32px',
          minHeight: '32px',
          overflow: 'visible',
          cursor: 'move',
          position: 'relative',
        }}
        viewBox="0 0 32 32"
      >
        <g>
          <g></g>
          <g>
            <g transform="translate(0.5,0.5)" style={{ visibility: 'visible' }}>
              <ellipse cx="16" cy="16" rx="14" ry="14" fill="#ffffff" stroke="#000000"></ellipse>
            </g>
          </g>
          <g></g>
          <g></g>
          <g></g>
        </g>
      </svg>
    </div>
  ),
  source: (
    <div className="m-generic-shape-svg">
      <svg xmlns="http://www.w3.org/2000/svg"
           style={{
             left: '0px',
             top: '0px',
             width: '100%',
             height: '100%',
             display: 'block',
             minWidth: '42px',
             minHeight: '42px',
             overflow: 'visible',
             cursor: 'move',
             position: 'relative',
           }}
           viewBox="0 0 32 32">
        <g>
          <g></g>
          <g>
            <g transform="translate(4,4)" style={{ visibility: 'visible' }}>
              <path d="M 0.5 21.2 L 23.5 21.2 L 12 0.5 Z" fill="#ffffff" stroke="#000000" strokeLinejoin="round"
                    strokeLinecap="round" strokeMiterlimit="10" pointerEvents="all"></path>
            </g>
          </g>
          <g></g>
          <g></g>
          <g></g>
        </g>
      </svg>
    </div>
  ),
  resourceConnection: (
    <svg xmlns="http://www.w3.org/2000/svg" width="40px" height="40px" viewBox="0 0 24 24" fill="none">
      <path
        d="M18 5C17.4477 5 17 5.44772 17 6C17 6.27642 17.1108 6.52505 17.2929 6.70711C17.475 6.88917 17.7236 7 18 7C18.5523 7 19 6.55228 19 6C19 5.44772 18.5523 5 18 5ZM15 6C15 4.34315 16.3431 3 18 3C19.6569 3 21 4.34315 21 6C21 7.65685 19.6569 9 18 9C17.5372 9 17.0984 8.8948 16.7068 8.70744L8.70744 16.7068C8.8948 17.0984 9 17.5372 9 18C9 19.6569 7.65685 21 6 21C4.34315 21 3 19.6569 3 18C3 16.3431 4.34315 15 6 15C6.46278 15 6.90157 15.1052 7.29323 15.2926L15.2926 7.29323C15.1052 6.90157 15 6.46278 15 6ZM6 17C5.44772 17 5 17.4477 5 18C5 18.5523 5.44772 19 6 19C6.55228 19 7 18.5523 7 18C7 17.7236 6.88917 17.475 6.70711 17.2929C6.52505 17.1108 6.27642 17 6 17Z"
        fill="#000000"
      />
    </svg>
  ),
};
